using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroInterrogazioni.Converter
{
	internal class ObjectCompararerConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
			=> value?.Equals(parameter);

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value is bool v && v)
				return parameter;
			else
				return BindingOperations.DoNothing;
		}
	}
}
