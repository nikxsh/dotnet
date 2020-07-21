using System;
using System.Collections.Generic;

namespace ObjectOriented.CreationalPatterns
{
	/// <summary>
	/// - Creational Patterns
	/// - In Factory pattern, we create object without exposing the creation logic. In this pattern, an interface is used for creating an object,
	///   but let subclass decide which class to instantiate. 
	/// - The creation of object is done when it is required. The Factory method allows a class later instantiation to subclasses.
	/// </summary>
	class FactoryPattern
	{
		public FactoryPattern()
		{
			Document[] documents = new Document[2];

			documents[0] = new Resume();
			documents[1] = new Report();

			foreach (var item in documents)
			{
				Console.WriteLine($"{item.GetType().Name}:");
				foreach (IPage page in item.Pages)
				{
					Console.WriteLine($"{page.GetType().Name} {page.Title}");
				}
			}

		}

		/// <summary>
		/// The 'Product' abstract class
		/// </summary>
		interface IPage
		{
			string Title { get; }
		}

		/// <summary>
		/// A 'ConcreteProduct' class
		/// </summary>
		class SkillsPage : IPage
		{
			public string Title
			{
				get
				{
					return "Dot Net";
				}
			}
		}

		/// <summary>
		/// A 'ConcreteProduct' class
		/// </summary>
		class EducationPage : IPage
		{
			public string Title
			{
				get
				{
					return "BE - IT";
				}
			}
		}

		/// <summary>
		/// A 'ConcreteProduct' class
		/// </summary>
		class ExperiencePage : IPage
		{
			public string Title
			{
				get
				{
					return "3.7 Years";
				}
			}
		}

		/// <summary>
		/// A 'ConcreteProduct' class
		/// </summary>
		class IntroductionPage : IPage
		{
			public string Title
			{
				get
				{
					return "My self Blah-Blah";
				}
			}
		}

		/// <summary>
		/// A 'ConcreteProduct' class
		/// </summary>
		class ResultsPage : IPage
		{
			public string Title
			{
				get
				{
					return "Fucking awesome";
				}
			}
		}

		/// <summary>
		/// A 'ConcreteProduct' class
		/// </summary>
		class ConclusionPage : IPage
		{
			public string Title
			{
				get
				{
					return "lol.. No conclusion, if you're engineer! :D";
				}
			}
		}

		/// <summary>
		/// A 'ConcreteProduct' class
		/// </summary>
		class SummaryPage : IPage
		{
			public string Title
			{
				get
				{
					return "In summer you will get summary!";
				}
			}
		}

		/// <summary>
		/// A 'ConcreteProduct' class
		/// </summary>
		class BibliographyPage : IPage
		{
			public string Title
			{
				get
				{
					return "Ohh I see...";
				}
			}
		}

		/// <summary>
		/// The 'Creator' abstract class
		/// </summary>
		abstract class Document
		{
			private List<IPage> _pages = new List<IPage>();

			public Document()
			{
				this.CreatePages();
			}

			public List<IPage> Pages
			{
				get { return _pages; }
			}

			public abstract void CreatePages();
		}

		/// <summary>
		/// A 'ConcreteCreator' class
		/// </summary>
		class Resume : Document
		{
			// Factory Method implementation
			public override void CreatePages()
			{
				Pages.Add(new SkillsPage());
				Pages.Add(new EducationPage());
				Pages.Add(new ExperiencePage());
			}
		}

		/// <summary>
		/// A 'ConcreteCreator' class
		/// </summary>
		class Report : Document
		{
			// Factory Method implementation
			public override void CreatePages()
			{
				Pages.Add(new IntroductionPage());
				Pages.Add(new ResultsPage());
				Pages.Add(new ConclusionPage());
				Pages.Add(new SummaryPage());
				Pages.Add(new BibliographyPage());
			}
		}
	}
}
