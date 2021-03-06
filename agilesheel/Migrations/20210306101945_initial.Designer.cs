﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using agilesheel.Models;

namespace agilesheel.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    [Migration("20210306101945_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("agilesheel.Models.Cinema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cinemas");
                });

            modelBuilder.Entity("agilesheel.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Is3D")
                        .HasColumnType("bit");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<string>("ParentalRating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PosterUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Synopsis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("agilesheel.Models.SeatRow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Seats")
                        .HasColumnType("int");

                    b.Property<int?>("TheaterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TheaterId");

                    b.ToTable("SeatRows");
                });

            modelBuilder.Entity("agilesheel.Models.Show", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<int?>("MovieId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TheaterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("TheaterId");

                    b.ToTable("Shows");
                });

            modelBuilder.Entity("agilesheel.Models.Theater", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CinemaId")
                        .HasColumnType("int");

                    b.Property<bool>("Has3D")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CinemaId");

                    b.ToTable("Theaters");
                });

            modelBuilder.Entity("agilesheel.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("SeatNumber")
                        .HasColumnType("int");

                    b.Property<int?>("SeatRowId")
                        .HasColumnType("int");

                    b.Property<int?>("ShowId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SeatRowId");

                    b.HasIndex("ShowId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("agilesheel.Models.SeatRow", b =>
                {
                    b.HasOne("agilesheel.Models.Theater", "Theater")
                        .WithMany("SeatRows")
                        .HasForeignKey("TheaterId");

                    b.Navigation("Theater");
                });

            modelBuilder.Entity("agilesheel.Models.Show", b =>
                {
                    b.HasOne("agilesheel.Models.Movie", "Movie")
                        .WithMany("Shows")
                        .HasForeignKey("MovieId");

                    b.HasOne("agilesheel.Models.Theater", "Theater")
                        .WithMany("Shows")
                        .HasForeignKey("TheaterId");

                    b.Navigation("Movie");

                    b.Navigation("Theater");
                });

            modelBuilder.Entity("agilesheel.Models.Theater", b =>
                {
                    b.HasOne("agilesheel.Models.Cinema", "Cinema")
                        .WithMany("Theaters")
                        .HasForeignKey("CinemaId");

                    b.Navigation("Cinema");
                });

            modelBuilder.Entity("agilesheel.Models.Ticket", b =>
                {
                    b.HasOne("agilesheel.Models.SeatRow", "SeatRow")
                        .WithMany("Tickets")
                        .HasForeignKey("SeatRowId");

                    b.HasOne("agilesheel.Models.Show", "Show")
                        .WithMany("Tickets")
                        .HasForeignKey("ShowId");

                    b.Navigation("SeatRow");

                    b.Navigation("Show");
                });

            modelBuilder.Entity("agilesheel.Models.Cinema", b =>
                {
                    b.Navigation("Theaters");
                });

            modelBuilder.Entity("agilesheel.Models.Movie", b =>
                {
                    b.Navigation("Shows");
                });

            modelBuilder.Entity("agilesheel.Models.SeatRow", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("agilesheel.Models.Show", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("agilesheel.Models.Theater", b =>
                {
                    b.Navigation("SeatRows");

                    b.Navigation("Shows");
                });
#pragma warning restore 612, 618
        }
    }
}
