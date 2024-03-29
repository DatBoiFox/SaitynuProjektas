﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SaitynuProjektas.Models;

namespace SaitynuProjektas.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SaitynuProjektas.Models.Director", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("birthDate");

                    b.Property<string>("name")
                        .IsRequired();

                    b.Property<string>("surname")
                        .IsRequired();

                    b.HasKey("id");

                    b.ToTable("Directors");
                });

            modelBuilder.Entity("SaitynuProjektas.Models.FavoriteMovie", b =>
                {
                    b.Property<int>("uselessId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("movieId");

                    b.Property<int>("userId");

                    b.HasKey("uselessId");

                    b.HasIndex("userId");

                    b.ToTable("FavoriteMovies");
                });

            modelBuilder.Entity("SaitynuProjektas.Models.Genre", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("name")
                        .IsRequired();

                    b.HasKey("id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("SaitynuProjektas.Models.Movie", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("directorid");

                    b.Property<DateTime>("releaseDate");

                    b.Property<string>("title")
                        .IsRequired();

                    b.HasKey("id");

                    b.HasIndex("directorid");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("SaitynuProjektas.Models.MovieGenre", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("genreId");

                    b.Property<int>("movieId");

                    b.HasKey("id");

                    b.HasIndex("genreId");

                    b.HasIndex("movieId");

                    b.ToTable("MovieGenres");
                });

            modelBuilder.Entity("SaitynuProjektas.Models.PlanToWatch", b =>
                {
                    b.Property<int>("uselessId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("movieId");

                    b.Property<int>("userId");

                    b.HasKey("uselessId");

                    b.HasIndex("movieId");

                    b.HasIndex("userId");

                    b.ToTable("PlanToWatches");
                });

            modelBuilder.Entity("SaitynuProjektas.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("name")
                        .IsRequired();

                    b.Property<string>("nickName")
                        .IsRequired();

                    b.Property<string>("password")
                        .IsRequired();

                    b.Property<string>("surname")
                        .IsRequired();

                    b.Property<string>("uerLevel")
                        .IsRequired();

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SaitynuProjektas.Models.UserScore", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("movieId");

                    b.Property<float>("score");

                    b.Property<int>("userId");

                    b.HasKey("id");

                    b.HasIndex("movieId");

                    b.HasIndex("userId");

                    b.ToTable("UserScores");
                });

            modelBuilder.Entity("SaitynuProjektas.Models.FavoriteMovie", b =>
                {
                    b.HasOne("SaitynuProjektas.Models.User")
                        .WithMany("favoriteMovies")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SaitynuProjektas.Models.Movie", b =>
                {
                    b.HasOne("SaitynuProjektas.Models.Director", "director")
                        .WithMany("movies")
                        .HasForeignKey("directorid")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SaitynuProjektas.Models.MovieGenre", b =>
                {
                    b.HasOne("SaitynuProjektas.Models.Genre", "genre")
                        .WithMany()
                        .HasForeignKey("genreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SaitynuProjektas.Models.Movie", "movie")
                        .WithMany("movieGenres")
                        .HasForeignKey("movieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SaitynuProjektas.Models.PlanToWatch", b =>
                {
                    b.HasOne("SaitynuProjektas.Models.Movie", "movie")
                        .WithMany()
                        .HasForeignKey("movieId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SaitynuProjektas.Models.User", "user")
                        .WithMany("planToWatchMovies")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SaitynuProjektas.Models.UserScore", b =>
                {
                    b.HasOne("SaitynuProjektas.Models.Movie", "movie")
                        .WithMany("userScores")
                        .HasForeignKey("movieId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SaitynuProjektas.Models.User", "user")
                        .WithMany("userScores")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
