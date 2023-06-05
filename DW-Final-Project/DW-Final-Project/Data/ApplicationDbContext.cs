using DW_Final_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.EntityFrameworkCore;

namespace DW_Final_Project.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}


		/// <summary>
		/// Data Seeds. Criam valores default na BD
		/// </summary>
		/// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
			builder.Entity<Models.Type>().HasData(
				new Models.Type { id = 1, description = "Admin" },
				new Models.Type { id = 2, description = "Client"}
			);
			builder.Entity<Product_Season>().HasData(
				new Product_Season { id = 1 , description="Spring"},
                new Product_Season { id = 2, description = "Summer" },
                new Product_Season { id = 3, description = "Fall" },
                new Product_Season { id = 4, description = "Winter" }

            );
        }

        // ********************************************
        // Criação das tabelas da BD
        // ********************************************
        public DbSet<Category> Category { get; set; }
		public DbSet<Order> Order { get; set; }
		public DbSet<OrderItem> OrderItem { get; set; }
		public DbSet<Person> Person { get; set; }
		public DbSet<Product> Product { get; set; }
		public DbSet<Product_Image> Product_Image { get; set; }
		public DbSet<Models.Type> Type { get; set; }
		public DbSet<User> User { get; set; }
		public DbSet<Product_Season> Product_Season { get; set; }
	}
}