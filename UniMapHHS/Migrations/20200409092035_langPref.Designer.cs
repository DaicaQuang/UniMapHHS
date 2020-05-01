﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniMapHHS.Models;

namespace UniMapHHS.Migrations
{
    [DbContext(typeof(DbAppContext))]
    [Migration("20200409092035_langPref")]
    partial class langPref
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UniMapHHS.Models.BuildingFloor", b =>
                {
                    b.Property<int>("BuildingFloorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Building")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Floor")
                        .HasColumnType("int");

                    b.HasKey("BuildingFloorId");

                    b.ToTable("BuildingFloors");
                });

            modelBuilder.Entity("UniMapHHS.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Title")
                        .HasColumnType("int");

                    b.HasKey("CategoryId");

                    b.HasIndex("Title");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("UniMapHHS.Models.Favourite", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.HasKey("Username", "LocationId");

                    b.HasIndex("LocationId");

                    b.HasIndex("Username");

                    b.ToTable("Favourites");
                });

            modelBuilder.Entity("UniMapHHS.Models.Glossary", b =>
                {
                    b.Property<int>("GlossaryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Dutch")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("English")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Spanish")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GlossaryId");

                    b.ToTable("Glossaries");
                });

            modelBuilder.Entity("UniMapHHS.Models.History", b =>
                {
                    b.Property<int>("HistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("HistoryId");

                    b.HasIndex("LocationId");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("UniMapHHS.Models.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BuildingFloorId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<bool>("Locked")
                        .HasColumnType("bit");

                    b.Property<int>("MaxCapacity")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocationId");

                    b.HasIndex("BuildingFloorId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("UniMapHHS.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("langPref")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UniMapHHS.Models.Category", b =>
                {
                    b.HasOne("UniMapHHS.Models.Glossary", "Glossary")
                        .WithMany()
                        .HasForeignKey("Title")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UniMapHHS.Models.Favourite", b =>
                {
                    b.HasOne("UniMapHHS.Models.Location", "Location")
                        .WithMany("Favourites")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniMapHHS.Models.User", "User")
                        .WithMany("favourites")
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UniMapHHS.Models.History", b =>
                {
                    b.HasOne("UniMapHHS.Models.Location", "Location")
                        .WithMany("Histories")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UniMapHHS.Models.Location", b =>
                {
                    b.HasOne("UniMapHHS.Models.BuildingFloor", "BuildingFloor")
                        .WithMany("Locations")
                        .HasForeignKey("BuildingFloorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniMapHHS.Models.Category", "Category")
                        .WithMany("Locations")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
