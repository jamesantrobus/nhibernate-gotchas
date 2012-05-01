namespace NHibernateGotchas.Tests
{
	using System;
	using System.Linq;

	using FluentNHibernate.Testing;

	using HibernatingRhinos.Profiler.Appender.NHibernate;

	using NHibernate;
	using NHibernate.Linq;
	using NHibernate.Tool.hbm2ddl;
	using NHibernateGotchas.Model;

	using NUnit.Framework;

	[TestFixture]
	public class DeveloperFixture
	{
		private ISession _session;

		[Test]
		public void VerifyDeveloperMappings()
		{
			new PersistenceSpecification<Developer>(_session)
				.CheckProperty(x => x.Name, "James")
				.CheckProperty(x => x.Framework, WebFramework.Django)
				.VerifyTheMappings();
		}

		[Test]
		public void Gotcha_CastingToIntMakesObjectDirty()
		{
			_session.Save(new Developer { Name = "Peter", Framework = WebFramework.MVC });
			_session.Save(new Developer { Name = "Brian", Framework = WebFramework.Rails });
			_session.Save(new Developer { Name = "Meg", Framework = WebFramework.Django });

			_session.Flush();
			_session.Clear();

			var allDevelopers = _session.Query<Developer>().ToList();
			var first = allDevelopers.First();
			first.Name = "James";
			_session.Save(first);
			
			// calling flush here, we would expect it to update our first developer only
			// in fact, it will update all developers currently in session (3, as we called ToList earlier)

			_session.Flush();
		}

		[SetUp]
		public virtual void SetUp()
		{
			var sessionFactory = NHibernateHelper.CreateSessionFactory();
			NHibernateProfiler.Initialize();

			_session = sessionFactory.OpenSession();

			new SchemaExport(NHibernateHelper.GetConfig())
					.Execute(true, true, false, _session.Connection, Console.Out);
		}

		[TearDown]
		public virtual void TearDown()
		{
			if (_session != null)
				_session.Dispose();
		}
	}
}