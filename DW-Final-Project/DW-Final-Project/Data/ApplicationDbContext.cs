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
				new Models.Type { id = 1, description = "admin" },
				new Models.Type { id = 2, description = "client"});
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
	}
}