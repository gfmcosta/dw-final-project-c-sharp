using DW_Final_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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
				new Models.Type { id = 2, description = "Cliente"}
			);
			builder.Entity<Product_Season>().HasData(
				new Product_Season { id = 1 , description="Primavera"},
                new Product_Season { id = 2, description = "Verão"},
                new Product_Season { id = 3, description = "Outono" },
                new Product_Season { id = 4, description = "Inverno" }

            );
			builder.Entity<User>().HasData(
				new User { id = 1, email = "goncalo.costa@gmail.com", password = "7efe01a7a37b674f902aaaa6385f991e72018563f9c4280691bbc593988703d4", typeFK = 1},
				new User { id = 2, email = "joao.goncalves@gmail.com", password = "63831aef4d4c7fc4f58d430ae5cf5fb6d9b04b475dcbd8df6a5b57db6ae841ee", typeFK = 1},
				new User { id = 3, email = "jose.silva@gmail.com", password = "dad91e6a5a72560ba402a95f2a4cc43f57f2d300a26d417585ae8491a47540cc", typeFK = 2}
            );
			builder.Entity<Person>().HasData(
				new Person { id = 1, name = "Gonçalo Costa", phoneNumber = "925863873", address = "Rua das Flores 31 2D", postalCode = "2605-141 BELAS", dataNasc = DateTime.ParseExact("24/01/2003", "dd/MM/yyyy", CultureInfo.InvariantCulture), gender = "M", imagePath = "default-m.png", userFK = 1 },
				new Person { id = 2, name = "João Gonçalves", phoneNumber = "924665908", address = "Rua das Papoilas 21 1Esq", postalCode = "2300-674 TOMAR", dataNasc = DateTime.ParseExact("23/05/2003", "dd/MM/yyyy", CultureInfo.InvariantCulture), gender = "M", imagePath = "default-m.png", userFK = 2 },
				new Person { id = 3, name = "José Silva", phoneNumber = "913765880", address = "Quinta do Contador 4", postalCode = "2300-313 TOMAR", dataNasc = DateTime.ParseExact("29/07/1976", "dd/MM/yyyy", CultureInfo.InvariantCulture), gender = "M", imagePath = "default-m.png", userFK = 3 }

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
        }
        // ********************************************
        // Criação das tabelas da BD
        // ********************************************
        public DbSet<Category> Category { get; set; }
		public DbSet<Order> Order { get; set; }
		public DbSet<OrderItem> OrderItem { get; set; }
		public DbSet<Person> Person { get; set; }
		public DbSet<Product> Product { get; set; }
		public DbSet<Models.Type> Type { get; set; }
		public DbSet<User> User { get; set; }
		public DbSet<Product_Season> Product_Season { get; set; }
	}
}