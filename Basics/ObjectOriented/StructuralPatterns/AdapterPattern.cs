using System;

namespace ObjectOriented.StructuralPatterns
{
	/// <summary>
	/// - Structural Patterns
	/// - Match interfaces of different classes
	/// - Convert the interface of a class into another interface that clients expect. 
	/// - Adapter lets classes work together that couldn't otherwise because of incompatible interfaces
	/// - To understand this definition, let's use a  real-world simple example. You know a language, "A". 
	///   You visit a place where language "B" is spoken. But you don't know how to speak that language. 
	///   So you meet a person who knows both languages, in other words A and B. So he can act as an adapter 
	///   for you to talk with the local people of that place and help you communicate with them.
	/// </summary>
	class AdapterPattern
	{
		public AdapterPattern()
		{
			var targetAdapter1 = new SpanishToEnglish(new Spanish());
			targetAdapter1.Speak();

			var targetAdapter2 = new HindiToEnglish(new Hindi());
			targetAdapter2.Speak();
		}

		public class Spanish
		{
			public string Hablar()
			{
				return "Hola Amigos!";
			}
		}

		public class Hindi
		{
			public string Namaste()

			{
				return "Namaste!";
			}
		}

		/// <summary>
		/// We can use class as well
		/// </summary>
		public interface ITargetAdapter
		{
			void Speak();
		}

		public class SpanishToEnglish : ITargetAdapter
		{
			private Spanish _spanish { get; set; }

			public SpanishToEnglish(Spanish _spanish)
			{
				this._spanish = _spanish;
			}

			public void Speak()
			{
				var sentence = _spanish.Hablar();
				//Translate to english
				Console.WriteLine($"{sentence} means 'Hello Friends!'");
			}
		}

		public class HindiToEnglish : ITargetAdapter
		{
			private Hindi _hindi { get; set; }

			public HindiToEnglish(Hindi _hindi)
			{
				this._hindi = _hindi;
			}

			public void Speak()
			{
				var sentence = _hindi.Namaste();
				//Translate to english
				Console.WriteLine($"{sentence} means 'Hello!'");
			}
		}
	}
}
