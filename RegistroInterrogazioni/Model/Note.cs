using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroInterrogazioni.Model
{
	internal class Note : IStudentNote
	{
		[Required]
		public DateTime DateTime { get; set; }
		[Required]
		public string NoteValue {  get; set; } = string.Empty;
	}
}
