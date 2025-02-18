using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RegistroInterrogazioni.ViewModels;

namespace RegistroInterrogazioni.Views;

public partial class SelectCourseView : UserControl
{
    public SelectCourseView()
    {
        InitializeComponent();
        DataContext = new SelectCourseViewModel();
	}
}