using Loggit.Entities.Events;
using Microsoft.EntityFrameworkCore;

namespace Loggit.Database
{
	public class LoggitDbContext : DbContext
	{
		public DbSet<TakeoffEvent> Takeoffs { get; set; }
		public DbSet<LandingEvent> Landings { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=./test.db");
		}
	}
}