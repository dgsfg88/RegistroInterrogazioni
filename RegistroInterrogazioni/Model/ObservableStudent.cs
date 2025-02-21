using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroInterrogazioni.Model
{
	internal partial class ObservableStudent : ObservableObject
	{
		private Student student;

		public int ID => student.ID;
		public string Name => student.Name;
		public string LastName => student.LastName;
		public ReadOnlyCollection<Grade> Grades => student.Grades.AsReadOnly();
		public int GradesCount => student.Grades.Count;
		public ReadOnlyCollection<GradeNote> GradeNotes => student.GradeNotes.AsReadOnly();
		public int GradeNotesCount => student.GradeNotes.Count;
		public ReadOnlyCollection<Note> Notes => student.Notes.AsReadOnly();
		public int NotesCount => student.Notes.Count;
		public bool IsDSA => student.IsDSA;
		public double GradesWeight => student.GradesWeight;
		public double GradeNotesWeight => student.GradeNotesWeight;
		public double PartecipationGrade => student.PartecipationGrade;

		[ObservableProperty]
		private int _drawPosition = 0;

		public double MeanGrade
		{
			get
			{
				int count = 0;
				double valueSum = 0;
				double weightSum = 0;

				foreach (var grade in Grades)
				{
					count++;
					double recordWeight = grade.GradeCost;

					valueSum += grade.GradeValue * recordWeight;
					weightSum += recordWeight;
				}

				return valueSum / weightSum;
			}
		}

		public ObservableStudent(Student student) 
		{ 
			this.student = student;
		}

		public void AddGrade(Grade grade)
		{
			student.Grades.Add(grade);
			OnPropertyChanged(nameof(Grades));
			OnPropertyChanged(nameof(GradesCount));
			OnPropertyChanged(nameof(GradesWeight));
			OnPropertyChanged(nameof(MeanGrade));
		}

		public void AddGradeNote(GradeNote gradeNote)
		{
			student.GradeNotes.Add(gradeNote);
			OnPropertyChanged(nameof(GradeNotes));
			OnPropertyChanged(nameof(GradeNotesCount));
			OnPropertyChanged(nameof(GradeNotesWeight));
			OnPropertyChanged(nameof(PartecipationGrade));
		}

		public void AddNote(Note note)
		{
			student.Notes.Add(note);
			OnPropertyChanged(nameof(Notes));
			OnPropertyChanged(nameof(NotesCount));
		}
	}
}
