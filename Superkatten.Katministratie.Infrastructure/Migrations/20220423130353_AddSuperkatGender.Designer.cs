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
    [Migration("20220423130353_AddSuperkatGender")]
    partial class AddSuperkatGender
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Superkatten.Katministratie.Infrastructure.Entities.GastgezinDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Gastgezinnen");
                });

            modelBuilder.Entity("Superkatten.Katministratie.Infrastructure.Entities.SuperkatDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Area")
                        .HasColumnType("int");

                    b.Property<int>("Behaviour")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("Birthday")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("CageNumber")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CatchDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CatchLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("GastgezinDtoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsKitten")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<bool>("Reserved")
                        .HasColumnType("bit");

                    b.Property<bool>("Retour")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("GastgezinDtoId");

                    b.ToTable("SuperKatten");
                });

            modelBuilder.Entity("Superkatten.Katministratie.Infrastructure.Entities.SuperkatDto", b =>
                {
                    b.HasOne("Superkatten.Katministratie.Infrastructure.Entities.GastgezinDto", null)
                        .WithMany("Superkatten")
                        .HasForeignKey("GastgezinDtoId");
                });

            modelBuilder.Entity("Superkatten.Katministratie.Infrastructure.Entities.GastgezinDto", b =>
                {
                    b.Navigation("Superkatten");
                });
#pragma warning restore 612, 618
        }
    }
}
