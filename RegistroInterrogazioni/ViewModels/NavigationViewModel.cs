using Avalonia;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RegistroInterrogazioni.Model;
using RegistroInterrogazioni.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroInterrogazioni.ViewModels
{
	internal partial class NavigationViewModel : ViewModelBase
	{
		[ObservableProperty]
		private object? _contentToShow;

		public NavigationViewModel()
		{
			ContentToShow = new SelectCourseView();
		}

		partial void OnContentToShowChanged(object? value)
		{
			if (value is StyledElement view &&
				view.DataContext is ViewModelBase viewModel) 
				viewModel.Parent = this;
		}

		protected override void OnReturnHome(RoutedEventArgs eventArgs)
		{
			ContentToShow = new SelectCourseView();
			eventArgs.Handled = true;
		}

		protected override void OnReturnBack(RoutedEventArgs eventArgs)
			=> OnReturnHome(eventArgs);

		public void ShowCourse(Course course)
		{
			ContentToShow = new ShowCourseView(course);
		}

		public void EditCourse(Course course)
		{
			ContentToShow = new EditCourseView(course);
		}
	}
}
