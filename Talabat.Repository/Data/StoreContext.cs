using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.Repository.Data.Config;

namespace Talabat.Repository.Data
{
	public class StoreContext  : DbContext
	{
		public StoreContext(DbContextOptions<StoreContext> options):base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//modelBuilder.ApplyConfiguration(new ProductConfigrations());
		//	modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		}
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductBrand> ProductBrands { get; set; }
		public DbSet<ProductType> ProductTypes { get; set; }

	
	}
}
