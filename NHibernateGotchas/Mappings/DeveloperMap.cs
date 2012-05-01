namespace NHibernateGotchas.Mappings
{
	using FluentNHibernate.Mapping;
	using NHibernateGotchas.Model;

	public class DeveloperMap : ClassMap<Developer>
	{
		public DeveloperMap()
		{
			Id(x => x.Id);
			Map(x => x.Name);
			Map(x => x.Framework).CustomType<int>();
		}
	}
}