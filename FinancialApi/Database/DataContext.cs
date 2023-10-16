using Microsoft.EntityFrameworkCore;

namespace FinancialApi.Database
{
	public class DataContext : DbContext
	{
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=financialdb;Trusted_Connection=True;TrustServerCertificate=True");
		}

		protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
		{
			configurationBuilder.Properties<decimal>().HavePrecision(18, 6);
		}

		public DbSet<Customer> Customers { get; set; }
		public DbSet<Account> Accounts { get; set; }
	}
}
