namespace NHibernateGotchas.Tests
{
	using System.Linq;

	using FluentNHibernate.Testing;
	using NHibernate.Linq;
	using Model;

	using NUnit.Framework;

	[TestFixture]
	public class DeveloperFixture : TestBase
	{
		[Test]
		public void VerifyDeveloperMappings()
		{
			new PersistenceSpecification<Developer>(Session)
				.CheckProperty(x => x.Name, "James")
				.CheckProperty(x => x.Framework, WebFramework.Django)
				.VerifyTheMappings();
		}

		[Test]
		public void Gotcha_CastingToIntMakesObjectDirty()
		{
			Session.Save(new Developer { Name = "Peter", Framework = WebFramework.MVC });
			Session.Save(new Developer { Name = "Brian", Framework = WebFramework.Rails });
			Session.Save(new Developer { Name = "Meg", Framework = WebFramework.Django });

			Session.Flush();
			Session.Clear();

			var allDevelopers = Session.Query<Developer>().ToList();
			var first = allDevelopers.First();
			first.Name = "James";
			Session.Save(first);
			
			// calling flush here, we would expect it to update our first developer only
			// in fact, it will update all developers currently in session (3, as we called ToList earlier)

			Session.Flush();
		}
	}
}