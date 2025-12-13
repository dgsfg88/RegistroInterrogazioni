using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RegistroInterrogazioni.Model
{
	internal class CourseManager
	{
		private static CourseManager instance = new CourseManager();
		public static CourseManager Instance
			=> instance;

		private CourseManager() { }

		public string WorkingPath { get; set; } = ".";

		protected string GetFilePath(string id)
		{
			return Path.Combine(WorkingPath, GetFileName(id));
		}

		protected string GetFileName(string id)
			=> $"course_{id}.json";

		public Course CreateOrLoad(string id)
		{
			if (File.Exists(GetFilePath(id)))
			{
				var course = LoadCourse(GetFilePath(id));
				if (course != null)
					return course;
			}
			return new Course { ID = id };
		}

		protected Course? LoadCourse(string path)
		{
			return JsonSerializer.Deserialize<Course>(
					File.ReadAllText(path));
		}

		public void Delete(string id)
		{
			File.Delete(GetFilePath(id));
		}

		public List<Course> GetAllCourses()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(WorkingPath);
			return directoryInfo.GetFiles("course_*.json")
				.Select(x => LoadCourse(x.FullName))
				.OfType<Course>()
				.ToList();
		}

		public void ExportGradesCSV(DateTime from, DateTime to, string path = ".")
		{
			var csvText = string.Join("\n",
			GetAllCourses().SelectMany(course =>
			course.Students.SelectMany(
				student => 
				GetStudentCSV(course.ClassName + " " + course.Name, from, to, student))));

			File.WriteAllText(Path.Combine(path, $"Export{DateTime.Now.ToString("yyyy.MM.dd")}.csv"),
				csvText);
		}

		private static IEnumerable<string> GetStudentCSV(string courseID, DateTime from, DateTime to, Student student)
		{
			var gradesCSV = student.Grades
				.Where(grade => grade.DateTime > from && grade.DateTime < to)
				.Select(grade => $"{grade.DateTime.ToString("yyyy.MM.dd.HH.ss")};{courseID};Grade;{student.LastName};{student.Name};{grade.GradeValue.ToString("0.##", CultureInfo.InvariantCulture)};{(grade.GradeCost * 100).ToString("0")};{grade.Notes}");

			var n_gradesCSV = student.GradeNotes
				.Where(grade => grade.DateTime > from && grade.DateTime < to)
				.Select(grade => $"{grade.DateTime.ToString("yyyy.MM.dd.HH.ss")};{courseID};GradeNote;{student.LastName};{student.Name};{grade.State};;{grade.Description}");

			var notesCSV = student.Notes
				.Where(grade => grade.DateTime > from && grade.DateTime < to)
				.Select(grade => $"{grade.DateTime.ToString("yyyy.MM.dd.HH.ss")};{courseID};Note;{student.LastName};{student.Name};;;{grade.NoteValue}");

			return gradesCSV.Concat(n_gradesCSV).Concat(notesCSV);
		}

		public void Save(Course course)
		{
			string json = JsonSerializer.Serialize(course,
				new JsonSerializerOptions() { WriteIndented = true, IgnoreReadOnlyProperties = true });
			File.WriteAllText(GetFilePath(course.ID), json);
		}
	}
}
