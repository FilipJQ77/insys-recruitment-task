﻿// <auto-generated />

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MovieLibrary.Data.Migrations
{
    [DbContext(typeof(MovieLibraryContext))]
    partial class MovieLibraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("MovieLibrary.Data.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MovieLibrary.Data.Entities.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ImdbRating")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieLibrary.Data.Entities.MovieCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MovieId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieCategories");
                });

            modelBuilder.Entity("MovieLibrary.Data.Entities.MovieCategory", b =>
                {
                    b.HasOne("MovieLibrary.Data.Entities.Category", "Category")
                        .WithMany("MovieCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieLibrary.Data.Entities.Movie", "Movie")
                        .WithMany("MovieCategories")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieLibrary.Data.Entities.Category", b =>
                {
                    b.Navigation("MovieCategories");
                });

            modelBuilder.Entity("MovieLibrary.Data.Entities.Movie", b =>
                {
                    b.Navigation("MovieCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
