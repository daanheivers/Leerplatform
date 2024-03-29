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
    [Migration("20220316140002_planningLessen")]
    partial class planningLessen
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Leerplatform.Models.Les", b =>
                {
                    b.Property<int>("LesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LokaalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PlanningId")
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

                    b.Property<string>("Naam")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Plaats")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LokaalId");

                    b.ToTable("Lokalen");
                });

            modelBuilder.Entity("Leerplatform.Models.Middel", b =>
                {
                    b.Property<int>("MiddelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naam")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MiddelId");

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

            modelBuilder.Entity("LokaalMiddel", b =>
                {
                    b.Property<string>("LokalenLokaalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MiddelenMiddelId")
                        .HasColumnType("int");

                    b.HasKey("LokalenLokaalId", "MiddelenMiddelId");

                    b.HasIndex("MiddelenMiddelId");

                    b.ToTable("LokaalMiddel");
                });

            modelBuilder.Entity("Leerplatform.Models.Les", b =>
                {
                    b.HasOne("Leerplatform.Models.Lokaal", "Lokaal")
                        .WithMany()
                        .HasForeignKey("LokaalId");

                    b.HasOne("Leerplatform.Models.Planning", "Planning")
                        .WithMany("Lessen")
                        .HasForeignKey("PlanningId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Leerplatform.Models.Vak", "Vak")
                        .WithMany("Lessen")
                        .HasForeignKey("VakId");

                    b.Navigation("Lokaal");

                    b.Navigation("Planning");

                    b.Navigation("Vak");
                });

            modelBuilder.Entity("LokaalMiddel", b =>
                {
                    b.HasOne("Leerplatform.Models.Lokaal", null)
                        .WithMany()
                        .HasForeignKey("LokalenLokaalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Leerplatform.Models.Middel", null)
                        .WithMany()
                        .HasForeignKey("MiddelenMiddelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Leerplatform.Models.Planning", b =>
                {
                    b.Navigation("Lessen");
                });

            modelBuilder.Entity("Leerplatform.Models.Vak", b =>
                {
                    b.Navigation("Lessen");
                });
#pragma warning restore 612, 618
        }
    }
}
