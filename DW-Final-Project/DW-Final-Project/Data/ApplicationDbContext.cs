using DW_Final_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DW_Final_Project.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
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