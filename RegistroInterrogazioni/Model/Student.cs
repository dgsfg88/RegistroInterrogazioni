using CommunityToolkit.Mvvm.ComponentModel;
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
		public bool IsDSA { get; set; } = false;

		public double GradesWeight 
		{ 
			get
			{
				var today = DateTime.Now;
				return Grades.Select(x =>
				((x.GradeValue < 6 ? x.GradeValue / 2 : x.GradeValue)
				* x.GradeCost) /
				Math.Max(0.5, (today - x.DateTime).TotalDays / 7)).Sum();
			} 
		}

		public double GradeNotesWeight
		{
			get
			{
				var today = DateTime.Now;
				return GradeNotes.Select(x => ((int)x.State) / 2.0 /
				Math.Max(0.5, (today - x.DateTime).TotalDays / 7)).Sum();
			}
		}

		public double PartecipationGrade
		{
			get
			{
				int p_count = 0;
				int n_count = 0;

				foreach (var gradeNote in GradeNotes)
				{
					switch (gradeNote.State)
					{
						case GradeNoteState.Negative:
							n_count++;
							break;
						case GradeNoteState.Positive:
							p_count++;
							break;
					};
				}

				return Math.Round(((p_count + n_count * -0.6) + n_count) / (p_count + n_count) * 40) / 4;
			}
		}
	}
}
