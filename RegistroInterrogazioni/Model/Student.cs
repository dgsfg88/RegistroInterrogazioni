using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroInterrogazioni.Model
{
	internal class Student
	{
		public int ID { get; set; }
		public string Name { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public List<Grade> Grades { get; set; } = new List<Grade>();
		public List<GradeNote> GradeNotes { get; set; } = new List<GradeNote>();
		public List<Note> Notes { get; set; } = new List<Note>();
	}
}
