using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RegistroInterrogazioni.Model
{
	public class Course
	{
		public string ID { get; set; } = string.Empty;
		/// <summary>
		/// Attualmente è previsto che gli studenti siano interni ai corsi, e quindi
		/// ripetuti nei vari file perché si prevede la gestione di poche classi e poche
		/// materie, tutto contenuto all'interno di un file JSON
		/// </summary>
		public List<Student> Students { get; set; } = new List<Student>();
		public string Name { get; set; } = string.Empty;
		public string ClassName { get; set; } = string.Empty;

		public void Save()
		{
			CourseManager.Instance.Save(this);
		}
	}
}
