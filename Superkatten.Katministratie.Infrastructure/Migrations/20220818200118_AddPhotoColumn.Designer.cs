﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Superkatten.Katministratie.Infrastructure.Persistence;

#nullable disable

namespace Superkatten.Katministratie.Infrastructure.Migrations
{
    [DbContext(typeof(SuperkattenDbContext))]
    [Migration("20220818200118_AddPhotoColumn")]
    partial class AddPhotoColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Superkatten.Katministratie.Domain.Entities.Gastgezin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Gastgezinnen");
                });

            modelBuilder.Entity("Superkatten.Katministratie.Domain.Entities.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Superkatten.Katministratie.Domain.Entities.MedicalProcedure", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ProcedureType")
                        .HasColumnType("int");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SuperkatId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("MedicalProcedures");
                });

            modelBuilder.Entity("Superkatten.Katministratie.Domain.Entities.Superkat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AgeCategory")
                        .HasColumnType("int");

                    b.Property<int>("Behaviour")
                        .HasColumnType("int");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CageNumber")
                        .HasColumnType("int");

                    b.Property<int>("CatArea")
                        .HasColumnType("int");

                    b.Property<DateTime>("CatchDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CatchLocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FoodType")
                        .HasColumnType("int");

                    b.Property<Guid?>("GastgezinId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("LitterType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("Reserved")
                        .HasColumnType("bit");

                    b.Property<bool>("Retour")
                        .HasColumnType("bit");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<bool>("WetFoodAllowed")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CatchLocationId");

                    b.ToTable("SuperKatten");
                });

            modelBuilder.Entity("Superkatten.Katministratie.Infrastructure.Entities.UserDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Permissions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Superkatten.Katministratie.Domain.Entities.Superkat", b =>
                {
                    b.HasOne("Superkatten.Katministratie.Domain.Entities.Location", "CatchLocation")
                        .WithMany()
                        .HasForeignKey("CatchLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CatchLocation");
                });
#pragma warning restore 612, 618
        }
    }
}
