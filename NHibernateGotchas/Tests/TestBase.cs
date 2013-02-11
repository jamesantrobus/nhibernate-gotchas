using System;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace NHibernateGotchas.Tests
{
	public abstract class TestBase
	{
		protected ISession Session { get; set; }

		[SetUp]
		public virtual void SetUp()
		{
			var sessionFactory = NHibernateHelper.CreateSessionFactory();
			NHibernateProfiler.Initialize();

			Session = sessionFactory.OpenSession();

			new SchemaExport(NHibernateHelper.GetConfig())
					.Execute(true, true, false, Session.Connection, Console.Out);
		}

		[TearDown]
		public virtual void TearDown()
		{
			if (Session != null)
				Session.Dispose();
		}
	}
}