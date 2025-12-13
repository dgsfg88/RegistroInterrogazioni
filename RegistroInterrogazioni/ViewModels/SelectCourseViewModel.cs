using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RegistroInterrogazioni.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroInterrogazioni.ViewModels
{
	internal partial class SelectCourseViewModel : ViewModelBase
	{
		[ObservableProperty]
		private ObservableCollection<Course> _courses;

		public SelectCourseViewModel()
		{
			_courses = new ObservableCollection<Course>(CourseManager.Instance.GetAllCourses());
		}

		[RelayCommand]
		private void CreateNewCourse()
		{
			if (Parent is NavigationViewModel navigationViewModel)
			{
				var id = Enumerable.Range(0, Courses.Count + 1).Select(x => x.ToString())
				.Except(Courses.Select(x => x.ID)).First();
				navigationViewModel.EditCourse(new Course() { ID = id });
			}
		}
		[RelayCommand]
		private void SelectCourse(Course course) 
		{
			if (Parent is NavigationViewModel navigationViewModel)
			{
				navigationViewModel.ShowCourse(course);
			}
		}
		[RelayCommand]
		private void DeleteCourse(Course course) 
		{
			Courses.Remove(course);
			CourseManager.Instance.Delete(course.ID);
		}
		[RelayCommand]
		private void DuplicateCourse(Course course)
		{
			if (Parent is NavigationViewModel navigationViewModel)
			{
				var id = Enumerable.Range(0, Courses.Count + 1).Select(x => x.ToString())
				.Except(Courses.Select(x => x.ID)).First();
				navigationViewModel.EditCourse(new Course() 
				{ 
					ID = id,
					Name = course.Name,
					ClassName = course.ClassName,
					Students = course.Students.Select(x=> new Student()
					{
						ID = x.ID,
						Name = x.Name,
						LastName = x.LastName,
					}).ToList(),
				});
			}
		}
		[RelayCommand]
		private void EditCourse(Course course) 
		{
			if (Parent is NavigationViewModel navigationViewModel)
			{
				navigationViewModel.EditCourse(course);
			}
		}

		[RelayCommand]
		private void ExportCSV()
		{
			CourseManager.Instance.ExportGradesCSV(DateTime.MinValue, DateTime.MaxValue);
		}
	}
}
