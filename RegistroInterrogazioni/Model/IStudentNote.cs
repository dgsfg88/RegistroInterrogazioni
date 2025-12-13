using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroInterrogazioni.Model
{
	internal interface IStudentNote
	{
		public int ID { get; set; }
		[Required]
		public DateTime DateTime { get; set; }
	}
}
