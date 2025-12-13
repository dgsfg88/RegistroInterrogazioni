using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroInterrogazioni.Model
{
	public enum GradeNoteState
	{
		None,
		Negative,
		Positive
	}
	public class GradeNote : IStudentNote
	{
		[Required]
		public DateTime DateTime { get; set; }
		public string Description { get; set; } = string.Empty;
		[Required]
		public GradeNoteState State { get; set; } = GradeNoteState.None;
		public int ID { get; set; } = -1;
	}
}
