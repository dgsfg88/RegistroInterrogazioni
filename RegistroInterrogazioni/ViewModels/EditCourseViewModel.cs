using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RegistroInterrogazioni.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RegistroInterrogazioni.ViewModels
{
	internal partial class EditCourseViewModel : ViewModelBase
	{
		[ObservableProperty]
		private Course _course;
		private string courseID;

		partial void OnCourseChanged(Course value)
		{
			Students = new ObservableCollection<Student>(value.Students);
		}

		[ObservableProperty]
		ObservableCollection<Student> _students;

		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(AddStudentCommand))]
		private string? _newStudentLastName;

		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(AddStudentCommand))]
		private string? _newStudentName;

		public EditCourseViewModel(string courseID)
		{
			this.courseID = courseID;
			_course = CourseManager.Instance.CreateOrLoad(courseID);
			_students = new ObservableCollection<Student>(_course.Students);
		}

		public EditCourseViewModel(Course course)
		{
			this.courseID = course.ID;
			_course = course;
			_students = new ObservableCollection<Student>(_course.Students);
		}

		private bool CanAddStudent() => !(string.IsNullOrWhiteSpace(NewStudentLastName) 
			|| string.IsNullOrWhiteSpace(NewStudentName));

		[RelayCommand]
		private void ApplyChanges()
		{
			//Aggiorna l'elenco degli studenti
			Course.Students = new List<Student>(Students);
			Course.Save();
		}
		[RelayCommand]
		private void RemoveStudent(Student student)
		{
			Students.Remove(student);
		}
		[RelayCommand(CanExecute = nameof(CanAddStudent))]
		private void AddStudent()
		{
			if (NewStudentName == null || NewStudentLastName == null) { return; }

			var id = Enumerable.Range(0, Students.Count + 1)
				.Except(Students.Select(x => x.ID)).FirstOrDefault();

			Student student = new Student()
			{
				ID = id,
				Name = NewStudentName,
				LastName = NewStudentLastName
			};
			NewStudentName = string.Empty;
			NewStudentLastName = string.Empty;
			Students.Add(student);
		}
	}
}
