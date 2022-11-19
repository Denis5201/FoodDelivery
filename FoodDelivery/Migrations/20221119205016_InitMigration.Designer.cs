﻿// <auto-generated />
using System;
using FoodDelivery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodDelivery.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20221119205016_InitMigration")]
    partial class InitMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FoodDelivery.Models.Basket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<Guid>("DishId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DishId");

                    b.HasIndex("UserId");

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("FoodDelivery.Models.Dish", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<bool>("Vegetarian")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("FoodDelivery.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DeliveryTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("FoodDelivery.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AmountVotes")
                        .HasColumnType("int");

                    b.Property<Guid>("DishId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("DishRating")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DishId")
                        .IsUnique();

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("FoodDelivery.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("birthDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FoodDelivery.Models.Basket", b =>
                {
                    b.HasOne("FoodDelivery.Models.Dish", "Dish")
                        .WithMany()
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodDelivery.Models.User", "User")
                        .WithMany("DishInBasket")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dish");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FoodDelivery.Models.Order", b =>
                {
                    b.HasOne("FoodDelivery.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FoodDelivery.Models.Rating", b =>
                {
                    b.HasOne("FoodDelivery.Models.Dish", "Dish")
                        .WithOne("Rating")
                        .HasForeignKey("FoodDelivery.Models.Rating", "DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dish");
                });

            modelBuilder.Entity("FoodDelivery.Models.Dish", b =>
                {
                    b.Navigation("Rating");
                });

            modelBuilder.Entity("FoodDelivery.Models.User", b =>
                {
                    b.Navigation("DishInBasket");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
