using CommunityToolkit.Mvvm.ComponentModel;
using RegistroInterrogazioni.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroInterrogazioni.ViewModels
{
	internal enum GradeSelected
	{
		Note,
		Grade,
		GradeNote
	}

	internal partial class ShowCourseViewModel : ViewModelBase
	{
		[ObservableProperty]
		private Course _Course;
		[ObservableProperty]
		private GradeSelected _GradeSelected = GradeSelected.GradeNote;
		[ObservableProperty]
		private string _Description = string.Empty;
		[ObservableProperty]
		private DateTimeOffset _GradeDate = DateTimeOffset.Now;
		[ObservableProperty]
		private int _GradeWeight = 100;

		public ShowCourseViewModel(Course course)
		{
			_Course = course;
		}
	}
}
