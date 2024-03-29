﻿// <auto-generated />
using System;
using Leerplatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Leerplatform.Migrations
{
    [DbContext(typeof(LeerplatformDbContext))]
    [Migration("20220310211703_DbInit")]
    partial class DbInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Leerplatform.Models.Les", b =>
                {
                    b.Property<int>("LesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LokaalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("PlanningId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Tijdstip")
                        .HasColumnType("datetime2");

                    b.Property<string>("VakId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LesId");

                    b.HasIndex("LokaalId");

                    b.HasIndex("PlanningId");

                    b.HasIndex("VakId");

                    b.ToTable("Lessen");
                });

            modelBuilder.Entity("Leerplatform.Models.Lokaal", b =>
                {
                    b.Property<string>("LokaalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Capaciteit")
                        .HasColumnType("int");

                    b.HasKey("LokaalId");

                    b.ToTable("Lokalen");
                });

            modelBuilder.Entity("Leerplatform.Models.Middel", b =>
                {
                    b.Property<int>("MiddelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LokaalId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MiddelId");

                    b.HasIndex("LokaalId");

                    b.ToTable("Middelen");
                });

            modelBuilder.Entity("Leerplatform.Models.Planning", b =>
                {
                    b.Property<int>("PlanningId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("PlanningId");

                    b.ToTable("Planningen");
                });

            modelBuilder.Entity("Leerplatform.Models.Vak", b =>
                {
                    b.Property<string>("VakId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Studiepunten")
                        .HasColumnType("int");

                    b.Property<string>("Titel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VakId");

                    b.ToTable("Vakken");
                });

            modelBuilder.Entity("Leerplatform.Models.Les", b =>
                {
                    b.HasOne("Leerplatform.Models.Lokaal", "Lokaal")
                        .WithMany()
                        .HasForeignKey("LokaalId");

                    b.HasOne("Leerplatform.Models.Planning", null)
                        .WithMany("Lessen")
                        .HasForeignKey("PlanningId");

                    b.HasOne("Leerplatform.Models.Vak", "Vak")
                        .WithMany()
                        .HasForeignKey("VakId");

                    b.Navigation("Lokaal");

                    b.Navigation("Vak");
                });

            modelBuilder.Entity("Leerplatform.Models.Middel", b =>
                {
                    b.HasOne("Leerplatform.Models.Lokaal", null)
                        .WithMany("Middelen")
                        .HasForeignKey("LokaalId");
                });

            modelBuilder.Entity("Leerplatform.Models.Lokaal", b =>
                {
                    b.Navigation("Middelen");
                });

            modelBuilder.Entity("Leerplatform.Models.Planning", b =>
                {
                    b.Navigation("Lessen");
                });
#pragma warning restore 612, 618
        }
    }
}
