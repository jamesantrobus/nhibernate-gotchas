namespace NHibernateGotchas.Model
{
	public class Developer
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual WebFramework Framework { get; set; }
		
		// unmapped property
		public virtual bool IsActive { get; set; }
	}
}