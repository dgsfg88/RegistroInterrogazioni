using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;

namespace RegistroInterrogazioni.ViewModels;

public partial class ViewModelBase : ObservableObject
{
	[ObservableProperty]
	private ViewModelBase? _parent = null;

	partial void OnParentChanged(ViewModelBase? oldValue, ViewModelBase? newValue)
	{
		if (oldValue != null)
			oldValue.Children.Remove(this);
		if (newValue != null && !newValue.Children.Contains(this))
			newValue.Children.Add(this);
	}

	protected List<ViewModelBase> Children { get; } = new List<ViewModelBase>();


	protected virtual void OnReturnHome(RoutedEventArgs eventArgs)
	{
	}
	protected virtual void OnReturnBack(RoutedEventArgs eventArgs)
	{
	}


	[RelayCommand()]
	protected void ReturnHome()
	{
		RoutedEventArgs eventArgs = new RoutedEventArgs();
		foreach (var child in Children)
		{
			child.OnReturnHome(eventArgs);
			if (eventArgs.Handled)
				return;
		}
		OnReturnHome(eventArgs);
		if (!eventArgs.Handled && Parent != null)
			Parent.ReturnHome();
	}
	[RelayCommand]
	protected void ReturnBack()
	{
		RoutedEventArgs eventArgs = new RoutedEventArgs();
		foreach (var child in Children)
		{
			child.OnReturnBack(eventArgs);
			if (eventArgs.Handled)
				return;
		}
		OnReturnBack(eventArgs);
		if (!eventArgs.Handled && Parent != null)
			Parent.ReturnBack();
	}
}
