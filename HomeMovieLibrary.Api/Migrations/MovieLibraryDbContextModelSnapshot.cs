﻿// <auto-generated />
using System;
using HomeMovieLibrary.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HomeMovieLibrary.Api.Migrations
{
    [DbContext(typeof(MovieLibraryDbContext))]
    partial class MovieLibraryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.2");

            modelBuilder.Entity("HomeMovieLibrary.Api.Models.DB.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("BirthDay")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeathDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("MainPhotoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MainPhotoId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("HomeMovieLibrary.Api.Models.DB.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DurationInSeconds")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("PosterId")
                        .HasColumnType("INTEGER");

                    b.Property<short>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ReleaseYear")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PosterId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("HomeMovieLibrary.Api.Models.DB.MovieActor", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActorId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MovieId", "ActorId");

                    b.HasIndex("ActorId");

                    b.ToTable("MovieActors");
                });

            modelBuilder.Entity("HomeMovieLibrary.Api.Models.DB.MovieDirector", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DirectorId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MovieId", "DirectorId");

                    b.HasIndex("DirectorId");

                    b.ToTable("MovieDirectors");
                });

            modelBuilder.Entity("HomeMovieLibrary.Api.Models.DB.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhysicalPath")
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("HomeMovieLibrary.Api.Models.DB.Author", b =>
                {
                    b.HasOne("HomeMovieLibrary.Api.Models.DB.Picture", "MainPhoto")
                        .WithMany()
                        .HasForeignKey("MainPhotoId");

                    b.Navigation("MainPhoto");
                });

            modelBuilder.Entity("HomeMovieLibrary.Api.Models.DB.Movie", b =>
                {
                    b.HasOne("HomeMovieLibrary.Api.Models.DB.Picture", "Poster")
                        .WithMany()
                        .HasForeignKey("PosterId");

                    b.Navigation("Poster");
                });

            modelBuilder.Entity("HomeMovieLibrary.Api.Models.DB.MovieActor", b =>
                {
                    b.HasOne("HomeMovieLibrary.Api.Models.DB.Author", "Actor")
                        .WithMany("MovieActors")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HomeMovieLibrary.Api.Models.DB.Movie", "Movie")
                        .WithMany("MovieActors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("HomeMovieLibrary.Api.Models.DB.MovieDirector", b =>
                {
                    b.HasOne("HomeMovieLibrary.Api.Models.DB.Author", "Director")
                        .WithMany("MovieDirectors")
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HomeMovieLibrary.Api.Models.DB.Movie", "Movie")
                        .WithMany("MovieDirectors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Director");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("HomeMovieLibrary.Api.Models.DB.Author", b =>
                {
                    b.Navigation("MovieActors");

                    b.Navigation("MovieDirectors");
                });

            modelBuilder.Entity("HomeMovieLibrary.Api.Models.DB.Movie", b =>
                {
                    b.Navigation("MovieActors");

                    b.Navigation("MovieDirectors");
                });
#pragma warning restore 612, 618
        }
    }
}
