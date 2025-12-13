using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroInterrogazioni.Model
{
	public class Grade : IStudentNote
	{
		[Required]
		public DateTime DateTime { get; set ; }
		public string Notes {  get; set ; } = string.Empty;
		[Required]
		public double GradeValue { get; set ; }
		[Required]
		public double GradeCost { get; set; } = 1;
		public int ID { get; set; } = -1;
	}
}
