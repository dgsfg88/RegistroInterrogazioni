using System;
using System.Collections.Generic;
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

		public string WorkingPath { get; set; } = "";

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

		public List<Course> GetAllCourses()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(WorkingPath);
			return directoryInfo.GetFiles("course_*.json")
				.Select(x => LoadCourse(x.FullName))
				.OfType<Course>()
				.ToList();
		}

		public void Save(Course course)
		{
			string json = JsonSerializer.Serialize(course,
				new JsonSerializerOptions() { WriteIndented = true });
			File.WriteAllText(GetFilePath(course.ID), json);
		}
	}
}
