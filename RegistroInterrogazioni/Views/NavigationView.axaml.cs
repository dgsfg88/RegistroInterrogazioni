using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RegistroInterrogazioni.ViewModels;

namespace RegistroInterrogazioni.Views;

public partial class NavigationView : UserControl
{
    public NavigationView()
    {
        InitializeComponent();
        DataContext = new NavigationViewModel();
	}
}