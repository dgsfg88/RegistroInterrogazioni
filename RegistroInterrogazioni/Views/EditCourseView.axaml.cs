using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RegistroInterrogazioni.ViewModels;

namespace RegistroInterrogazioni.Views;

public partial class EditCourseView : UserControl
{
    public EditCourseView() : this("0") { }
    public EditCourseView(string courseID)
    {
        InitializeComponent();
        DataContext = new EditCourseViewModel(courseID);
    }
}