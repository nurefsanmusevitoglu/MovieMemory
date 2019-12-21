using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Windows.Data;
using MovieMemory.Models;

namespace MovieMemory.Models
{
    public class MMContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieList> MovieLists { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<PersonalMovieList> PersonalMovieLists { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Update> Updates { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Friend>().HasKey(f => new { f.UserId1, f.UserId2 });
            modelBuilder.Entity<Friend>()
                .HasRequired(f => f.User1)
                .WithMany()
                .HasForeignKey(f => f.UserId1);
            modelBuilder.Entity<Friend>()
                .HasRequired(f => f.User2)
                .WithMany()
                .HasForeignKey(f => f.UserId2)
                .WillCascadeOnDelete(false);
        }
    }
}