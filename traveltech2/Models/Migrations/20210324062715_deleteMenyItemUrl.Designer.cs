﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using traveltech2.Models.Data;

namespace traveltech2.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210324062715_deleteMenyItemUrl")]
    partial class deleteMenyItemUrl
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("traveltech2.Models.App", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("HeadID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HeadID");

                    b.ToTable("Apps");
                });

            modelBuilder.Entity("traveltech2.Models.Drop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Drops");
                });

            modelBuilder.Entity("traveltech2.Models.Head", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("LogoID")
                        .HasColumnType("int");

                    b.Property<int?>("MenuID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LogoID");

                    b.HasIndex("MenuID");

                    b.ToTable("Heads");
                });

            modelBuilder.Entity("traveltech2.Models.Logo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Logos");
                });

            modelBuilder.Entity("traveltech2.Models.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DropID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DropID");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("traveltech2.Models.App", b =>
                {
                    b.HasOne("traveltech2.Models.Head", "Head")
                        .WithMany()
                        .HasForeignKey("HeadID");
                });

            modelBuilder.Entity("traveltech2.Models.Head", b =>
                {
                    b.HasOne("traveltech2.Models.Logo", "Logo")
                        .WithMany()
                        .HasForeignKey("LogoID");

                    b.HasOne("traveltech2.Models.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuID");
                });

            modelBuilder.Entity("traveltech2.Models.Menu", b =>
                {
                    b.HasOne("traveltech2.Models.Drop", "Drop")
                        .WithMany()
                        .HasForeignKey("DropID");
                });
#pragma warning restore 612, 618
        }
    }
}
