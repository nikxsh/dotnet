using System;

namespace Tools.Models
{
	public interface IStudent
	{
		void Display();
	}

	[Help("http://www.dummy.com/100", Topic = "Class A")]
	public class Student : IStudent, IEquatable<Student>, IComparable<Student>
	{
		public int StudentId { get; set; }
		public string Name { get; set; }
		public int Marks { get; set; }
		public Grade StudentGrade { get; set; }
		public int CompareChoice { get; set; }
		public string[] AssignedSubject { get; set; }

		public string this[int index]
		{
			get => AssignedSubject[index];
			set => AssignedSubject[index] = value;
		}

		public void Display()
		{
			Console.WriteLine($"{StudentId,3}:{Name,-20}:{Marks,3:C}:{StudentGrade.ToString(),3:C}");
		}

		//The Deconstruct method of a class, structure, or interface also allows you to retrieve and deconstruct 
		//a specific set of data from an object
		public void Deconstruct(out int studentId, out string name, out int marks, out Grade grade)
		{
			studentId = StudentId;
			name = Name;
			marks = Marks;
			grade = StudentGrade;
		}

		public bool Equals(Student other)
		{
			if (other == null)
				return false;
			else if (Marks == other.Marks && StudentId == other.StudentId)
				return true;
			else
				return false;
		}

		public int CompareTo(Student other)
		{
			if (CompareChoice == 0)
			{
				if (StudentId == other.StudentId)
					return 0;
				else if (StudentId > other.StudentId)
					return 1;
				else
					return -1;
			}
			else
			{
				if (Marks == other.Marks)
					return 0;
				else if (Marks > other.Marks)
					return 1;
				else
					return -1;
			}
		}
	}

	public enum Grade
	{
		A,
		B,
		C,
		D
	}
}
