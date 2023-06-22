﻿// <auto-generated />
using System;
using DW_Final_Project.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DW_Final_Project.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.Property<int>("categoryListid")
                        .HasColumnType("int");

                    b.Property<int>("productListid")
                        .HasColumnType("int");

                    b.HasKey("categoryListid", "productListid");

                    b.HasIndex("productListid");

                    b.ToTable("CategoryProduct");
                });

            modelBuilder.Entity("DW_Final_Project.Models.Category", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("id");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            id = 1,
                            description = "T-Shirt"
                        },
                        new
                        {
                            id = 2,
                            description = "Sweatshirt"
                        },
                        new
                        {
                            id = 3,
                            description = "Hoodie"
                        },
                        new
                        {
                            id = 4,
                            description = "Camisola"
                        },
                        new
                        {
                            id = 5,
                            description = "Calças"
                        },
                        new
                        {
                            id = 6,
                            description = "Calções"
                        },
                        new
                        {
                            id = 7,
                            description = "Calçado"
                        },
                        new
                        {
                            id = 8,
                            description = "Acessórios"
                        });
                });

            modelBuilder.Entity("DW_Final_Project.Models.Order", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("IVA")
                        .HasColumnType("int");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<int>("personFK")
                        .HasColumnType("int");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id");

                    b.HasIndex("personFK");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("DW_Final_Project.Models.OrderItem", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("orderFK")
                        .HasColumnType("int");

                    b.Property<int>("productFK")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<string>("size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("totalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id");

                    b.HasIndex("orderFK");

                    b.HasIndex("productFK");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("DW_Final_Project.Models.Person", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("dataNasc")
                        .HasColumnType("datetime2");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("postalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("DW_Final_Project.Models.Product", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<int>("seasonFK")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("seasonFK");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            id = 1,
                            description = "T-shirt branca básica",
                            imagePath = "default-c.png",
                            name = "T-Shirt Branca",
                            price = 5.0m,
                            quantity = 10,
                            seasonFK = 2
                        },
                        new
                        {
                            id = 2,
                            description = "T-shirt cinza básica",
                            imagePath = "default-c.png",
                            name = "T-Shirt Cinza",
                            price = 5.0m,
                            quantity = 10,
                            seasonFK = 2
                        },
                        new
                        {
                            id = 3,
                            description = "T-shirt azul básica",
                            imagePath = "default-c.png",
                            name = "T-Shirt Azul",
                            price = 5.0m,
                            quantity = 10,
                            seasonFK = 2
                        },
                        new
                        {
                            id = 4,
                            description = "Hoddie Vermelho básico",
                            imagePath = "default-c.png",
                            name = "Hoodie Vermelho",
                            price = 15.0m,
                            quantity = 20,
                            seasonFK = 4
                        },
                        new
                        {
                            id = 5,
                            description = "Hoddie Azul básico",
                            imagePath = "default-c.png",
                            name = "Hoodie Azul",
                            price = 15.0m,
                            quantity = 20,
                            seasonFK = 4
                        },
                        new
                        {
                            id = 6,
                            description = "Hoddie Amarelo básico",
                            imagePath = "default-c.png",
                            name = "Hoodie Amarelo",
                            price = 15.0m,
                            quantity = 20,
                            seasonFK = 4
                        },
                        new
                        {
                            id = 7,
                            description = "Camisola branca manga comprida básica",
                            imagePath = "default-c.png",
                            name = "Camisola Branca",
                            price = 5.0m,
                            quantity = 10,
                            seasonFK = 2
                        },
                        new
                        {
                            id = 8,
                            description = "Camisola preta manga comprida básica",
                            imagePath = "default-c.png",
                            name = "Camisola Preta",
                            price = 7.49m,
                            quantity = 4,
                            seasonFK = 1
                        },
                        new
                        {
                            id = 9,
                            description = "Camisola rosa manga comprida básica",
                            imagePath = "default-c.png",
                            name = "Camisola Rosa",
                            price = 7.49m,
                            quantity = 4,
                            seasonFK = 1
                        });
                });

            modelBuilder.Entity("DW_Final_Project.Models.Product_Season", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Product_Season");

                    b.HasData(
                        new
                        {
                            id = 1,
                            description = "Primavera"
                        },
                        new
                        {
                            id = 2,
                            description = "Verão"
                        },
                        new
                        {
                            id = 3,
                            description = "Outono"
                        },
                        new
                        {
                            id = 4,
                            description = "Inverno"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.HasOne("DW_Final_Project.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("categoryListid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DW_Final_Project.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("productListid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DW_Final_Project.Models.Order", b =>
                {
                    b.HasOne("DW_Final_Project.Models.Person", "Person")
                        .WithMany("orderList")
                        .HasForeignKey("personFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("DW_Final_Project.Models.OrderItem", b =>
                {
                    b.HasOne("DW_Final_Project.Models.Order", "order")
                        .WithMany("orderItemList")
                        .HasForeignKey("orderFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DW_Final_Project.Models.Product", "product")
                        .WithMany("orderItemList")
                        .HasForeignKey("productFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("order");

                    b.Navigation("product");
                });

            modelBuilder.Entity("DW_Final_Project.Models.Product", b =>
                {
                    b.HasOne("DW_Final_Project.Models.Product_Season", "Season")
                        .WithMany("productList")
                        .HasForeignKey("seasonFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Season");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DW_Final_Project.Models.Order", b =>
                {
                    b.Navigation("orderItemList");
                });

            modelBuilder.Entity("DW_Final_Project.Models.Person", b =>
                {
                    b.Navigation("orderList");
                });

            modelBuilder.Entity("DW_Final_Project.Models.Product", b =>
                {
                    b.Navigation("orderItemList");
                });

            modelBuilder.Entity("DW_Final_Project.Models.Product_Season", b =>
                {
                    b.Navigation("productList");
                });
#pragma warning restore 612, 618
        }
    }
}
