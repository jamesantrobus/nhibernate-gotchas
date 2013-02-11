using FluentNHibernate.Testing;
using NHibernateGotchas.Model;
using NUnit.Framework;

namespace NHibernateGotchas.Tests
{
	public class PerilsOfDefaultValues : TestBase
	{
		// example: the boolean IsActive property is not mapped

		// testing the property with a default value will cause the persistence specification to pass (incorrect)

		// testing with a non-default value will (correctly) cause the test to fail

		[Test]
		[TestCase(false, Description = "False positive")]
		[TestCase(true, Description = "Failing test (correct)")]
		public void VerifyIsActiveFieldWithoutMappingTheColumn(bool isActive)
		{
			new PersistenceSpecification<Developer>(Session)
				.CheckProperty(x => x.Name, "James")
				.CheckProperty(x => x.Framework, WebFramework.Django)
				.CheckProperty(x => x.IsActive, isActive)
				.VerifyTheMappings();
		}
	}
}