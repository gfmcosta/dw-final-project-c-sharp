using DW_Final_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Reflection.Emit;

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
            builder.Entity<IdentityUserLogin<string>>().HasNoKey();

            builder.Entity<Product_Season>().HasData(
				new Product_Season { id = 1 , description="Primavera"},
                new Product_Season { id = 2, description = "Verão"},
                new Product_Season { id = 3, description = "Outono" },
                new Product_Season { id = 4, description = "Inverno" }

            );
			builder.Entity<Category>().HasData(
				new Category { id = 1, description = "T-Shirt"},
				new Category { id = 2, description = "Sweatshirt"},
				new Category { id = 3, description = "Hoodie"},
				new Category { id = 4, description = "Camisola"},
				new Category { id = 5, description = "Calças"},
				new Category { id = 6, description = "Calções"},
				new Category { id = 7, description = "Calçado"},
				new Category { id = 8, description = "Acessórios"}
			);

			builder.Entity<Product>().HasData(
				new Product { id = 1, name="T-Shirt Branca", description="T-shirt branca básica",quantity=10,price=5.0M, imagePath="default-c.png", seasonFK=2},
				new Product { id = 2, name="T-Shirt Cinza", description="T-shirt cinza básica",quantity=10,price=5.0M, imagePath = "default-c.png", seasonFK =2},
				new Product { id = 3, name="T-Shirt Azul", description="T-shirt azul básica",quantity=10,price=5.0M, imagePath = "default-c.png", seasonFK=2},
				new Product { id = 4, name="Hoodie Vermelho", description="Hoddie Vermelho básico",quantity=20,price=15.0M, imagePath = "default-c.png", seasonFK =4},
				new Product { id = 5, name= "Hoodie Azul", description= "Hoddie Azul básico", quantity=20,price=15.0M, imagePath = "default-c.png", seasonFK =4},
				new Product { id = 6, name= "Hoodie Amarelo", description= "Hoddie Amarelo básico", quantity=20,price=15.0M, imagePath = "default-c.png", seasonFK =4},
				new Product { id = 7, name="Camisola Branca", description="Camisola branca manga comprida básica",quantity=10,price=5.0M, imagePath = "default-c.png", seasonFK =2},
				new Product { id = 8, name= "Camisola Preta", description= "Camisola preta manga comprida básica", quantity=4,price=7.49M, imagePath = "default-c.png", seasonFK =1},
				new Product { id = 9, name= "Camisola Rosa", description= "Camisola rosa manga comprida básica", quantity=4,price=7.49M, imagePath = "default-c.png", seasonFK =1}
			);
            base.OnModelCreating(builder);

        }
        // ********************************************
        // Criação das tabelas da BD
        // ********************************************
        public DbSet<Category> Category { get; set; }
		public DbSet<Order> Order { get; set; }
		public DbSet<OrderItem> OrderItem { get; set; }
		public DbSet<Person> Person { get; set; }
		public DbSet<Product> Product { get; set; }
		public DbSet<Product_Season> Product_Season { get; set; }
	}
}