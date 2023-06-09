﻿// <auto-generated />
using System;
using MKFood.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MkFoodWeb.Migrations
{
    [DbContext(typeof(MKFoodDbContext))]
    [Migration("20230320081913_upadtBill")]
    partial class upadtBill
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MKFood.DB.Models.Bill", b =>
                {
                    b.Property<int>("BillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BillId"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<double?>("Fee")
                        .HasColumnType("float");

                    b.Property<bool>("Open")
                        .HasColumnType("bit");

                    b.Property<bool>("Success")
                        .HasColumnType("bit");

                    b.Property<double?>("SumCost")
                        .HasColumnType("float");

                    b.Property<double?>("Tax")
                        .HasColumnType("float");

                    b.HasKey("BillId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("MKFood.DB.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MKFood.DB.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("MKFood.DB.Models.Food", b =>
                {
                    b.Property<int>("FoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FoodId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FoodId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("MKFood.DB.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int>("BillId")
                        .HasColumnType("int");

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("BillId");

                    b.HasIndex("FoodId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("MKFood.DB.Models.Price", b =>
                {
                    b.Property<int>("PriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PriceId"));

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<bool>("Expierd")
                        .HasColumnType("bit");

                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.HasKey("PriceId");

                    b.HasIndex("FoodId");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("MKFood.DB.Models.Bill", b =>
                {
                    b.HasOne("MKFood.DB.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("MKFood.DB.Models.Food", b =>
                {
                    b.HasOne("MKFood.DB.Models.Category", "Category")
                        .WithMany("Foods")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("MKFood.DB.Models.Order", b =>
                {
                    b.HasOne("MKFood.DB.Models.Bill", "Bill")
                        .WithMany("orders")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MKFood.DB.Models.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Food");
                });

            modelBuilder.Entity("MKFood.DB.Models.Price", b =>
                {
                    b.HasOne("MKFood.DB.Models.Food", "Food")
                        .WithMany("Prices")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");
                });

            modelBuilder.Entity("MKFood.DB.Models.Bill", b =>
                {
                    b.Navigation("orders");
                });

            modelBuilder.Entity("MKFood.DB.Models.Category", b =>
                {
                    b.Navigation("Foods");
                });

            modelBuilder.Entity("MKFood.DB.Models.Food", b =>
                {
                    b.Navigation("Prices");
                });
#pragma warning restore 612, 618
        }
    }
}
