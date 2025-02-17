using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroInterrogazioni.Model
{
	internal enum GradeNoteState
	{
		None,
		Negative,
		Positive
	}
	internal class GradeNote : IStudentNote
	{
		[Required]
		public DateTime DateTime { get; set; }
		public string Description { get; set; } = string.Empty;
		[Required]
		public GradeNoteState State { get; set; } = GradeNoteState.None;
	}
}
