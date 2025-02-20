using Avalonia.Data.Converters;
using RegistroInterrogazioni.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroInterrogazioni.Converter
{
	internal static class GradeTypeToVisibilityConverters
	{
		public static FuncValueConverter<GradeSelected, bool> IsNotNote { get; } =
			new FuncValueConverter<GradeSelected, bool>(gradeType => gradeType != GradeSelected.Note);
		public static FuncValueConverter<GradeSelected, bool> IsGradeNote { get; } =
			new FuncValueConverter<GradeSelected, bool>(gradeType => gradeType == GradeSelected.GradeNote);
		public static FuncValueConverter<GradeSelected, bool> IsGrade { get; } =
			new FuncValueConverter<GradeSelected, bool>(gradeType => gradeType == GradeSelected.Grade);
	}
}
