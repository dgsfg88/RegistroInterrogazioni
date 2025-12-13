using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RegistroInterrogazioni.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroInterrogazioni.ViewModels
{
	public enum GradeSelected
	{
		Note,
		Grade,
		GradeNote
	}

	public partial class ShowCourseViewModel : ViewModelBase
	{
		private Random random = new Random();

		[ObservableProperty]
		private Course _Course;
		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(AddGradeCommand))]
		private GradeSelected _GradeSelected = GradeSelected.GradeNote;
		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(AddGradeCommand))]
		private string _Description = string.Empty;
		[ObservableProperty]
		private DateTimeOffset _GradeDate = DateTimeOffset.Now;
		[ObservableProperty]
		private int _GradeWeight = 100;
		[ObservableProperty]
		private double _GradeValue = 6;
		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(AddGradeCommand))]
		private GradeNoteState _GradeNoteState = GradeNoteState.None;
		[ObservableProperty]
		private ObservableCollection<ObservableStudent> _Students;

		public List<double> GradeValues { get; } =
			Enumerable.Range(0, 41).Select(x => x / 4.0).ToList();
		public List<GradeNoteState> GradeNoteValues { get; } =
			[GradeNoteState.Negative, GradeNoteState.Positive];

		partial void OnCourseChanged(Course value)
		{
			Students = new ObservableCollection<ObservableStudent>(
				value.Students.Select(x => new ObservableStudent(x)));
		}

		public ShowCourseViewModel() : this(new Course()) { }

		public ShowCourseViewModel(Course course)
		{
			_Course = course;
			_Students = new ObservableCollection<ObservableStudent>(
				course.Students.Select(x =>  new ObservableStudent(x)));
		}

		private bool IsAddGradeEnabled()
		{
			switch (GradeSelected)
			{
				case GradeSelected.GradeNote:
					return GradeNoteState != GradeNoteState.None;
				case GradeSelected.Note:
					return !string.IsNullOrWhiteSpace(Description);
				default:
				case GradeSelected.Grade:
					return true;
			}
		}

		[RelayCommand]
		private void AssesmentDraw()
		{
			WeightedShuffle((x) => x.GradesWeight);
		}

		[RelayCommand]
		private void QuestionDraw()
		{
			WeightedShuffle((x) => x.GradeNotesWeight);
		}

		private List<int> WeightedShuffle(Func<ObservableStudent, double> getWeight)
		{
			var weights = Students.Select(x => (1 + getWeight(x)) / (x.IsDSA ? 0.75 : 1)).ToList();

			var order = Enumerable.Range(0, weights.Count)
			.OrderByDescending(i => Math.Pow(random.NextDouble(), 1.0 / weights[i]))
			.ToList();

			for (int i = 0; i < order.Count; i++)
				Students[order[i]].DrawPosition = order.Count - i;

			return order;
		}

		[RelayCommand(CanExecute=nameof(IsAddGradeEnabled))]
		private void AddGrade(ObservableStudent student)
		{
			switch (GradeSelected)
			{
				case GradeSelected.GradeNote:
					GradeNote gradeNote = new GradeNote()
					{
						DateTime = GradeDate.DateTime,
						Description = Description,
						State = GradeNoteState
					};
					student.AddGradeNote(gradeNote);
					break;
				case GradeSelected.Note:
					Note note = new Note() { DateTime = GradeDate.DateTime, NoteValue = Description };
					student.AddNote(note);
					break;
				case GradeSelected.Grade:
					Grade grade = new Grade()
					{
						DateTime = GradeDate.DateTime,
						Notes = Description,
						GradeCost = GradeWeight / 100.0,
						GradeValue = GradeValue
					};
					student.AddGrade(grade);
					break;
			}

			Course.Save();
		}
	}
}
