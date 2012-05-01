namespace NHibernateGotchas
{
	using FluentNHibernate.Cfg;
	using FluentNHibernate.Cfg.Db;

	using NHibernate;
	using NHibernate.Cfg;

	using NHibernateGotchas.Mappings;

	public class NHibernateHelper
	{
		public static ISessionFactory CreateSessionFactory()
		{
			return GetConfig().BuildSessionFactory();
		}

		public static Configuration GetConfig()
		{
			return GetMappings()
				.Database(SQLiteConfiguration.Standard.InMemory().ShowSql().FormatSql())
				.BuildConfiguration();
		}

		private static FluentConfiguration GetMappings()
		{
			return Fluently.Configure()
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<DeveloperMap>());
		}
	}
}
