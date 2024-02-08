﻿// <auto-generated />
using System;
using Financify.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Financify.Migrations
{
    [DbContext(typeof(Database))]
    [Migration("20240208021854_Category")]
    partial class Category
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.21");

            modelBuilder.Entity("Financify.Models.Category", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("ColorHEX")
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Financify.Models.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CompletionDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("GoalId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("InitialValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("TargetValue")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasAlternateKey("GoalId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("Financify.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("GoalId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsExpense")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MinimalDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasAlternateKey("TransactionId");

                    b.HasIndex("CategoryName");

                    b.HasIndex("GoalId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Financify.Models.Transaction", b =>
                {
                    b.HasOne("Financify.Models.Category", "Category")
                        .WithMany("Transactions")
                        .HasForeignKey("CategoryName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Financify.Models.Goal", "Goal")
                        .WithMany("Transactions")
                        .HasForeignKey("GoalId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Category");

                    b.Navigation("Goal");
                });

            modelBuilder.Entity("Financify.Models.Category", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Financify.Models.Goal", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
