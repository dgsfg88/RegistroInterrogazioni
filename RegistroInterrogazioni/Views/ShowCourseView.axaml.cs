using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RegistroInterrogazioni.Model;
using RegistroInterrogazioni.ViewModels;

namespace RegistroInterrogazioni.Views;

public partial class ShowCourseView : UserControl
{
    internal ShowCourseView(Course course)
    {
        InitializeComponent();
        DataContext = new ShowCourseViewModel(course);
    }
}