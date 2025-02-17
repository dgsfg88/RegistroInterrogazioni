using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RegistroInterrogazioni.ViewModels;

namespace RegistroInterrogazioni;

public partial class EditCourseView : UserControl
{
    public EditCourseView(string courseID)
    {
        InitializeComponent();
        DataContext = new EditCourseViewModel(courseID);
    }
}