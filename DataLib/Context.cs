﻿using ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLib {
    public class Context : DbContext {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Document> Documents { get; set; } = null!;
        public DbSet<ContactInfo> Contacts { get; set; } = null!;

        public string DbPath { get; }

        public Context() {
            DbPath = "storage.db";
        }

        public Context(String DataSource) {
            DbPath = DataSource;

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // primary keys
            modelBuilder.Entity<User>().HasKey(t => t.Id);
            modelBuilder.Entity<Project>().HasKey(t => t.Id);
            modelBuilder.Entity<Document>().HasKey(t => t.Id);
            modelBuilder.Entity<ContactInfo>().HasKey(t => t.Id);

            // one to zero or one 
            modelBuilder.Entity<ContactInfo>()
                .Property(u => u.Id);
            modelBuilder.Entity<User>()
                .HasOne(ci => ci.ContactInfo)
                .WithOne(u => u.User)
                .HasForeignKey<ContactInfo>(u => u.Id)
                .OnDelete(DeleteBehavior.Cascade);

            // one to many
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Documents)
                .WithOne(p => p.Project)
                .HasForeignKey(b => b.Id)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            // many to many
            modelBuilder.Entity<User>()
                .HasMany(p => p.Projects)
                .WithMany(u => u.Users);

            // constraints
            modelBuilder.Entity<User>()
                .HasAlternateKey(u => new { u.Name, u.Email });  // unique
                                                                 // length
            modelBuilder.Entity<User>()
                .Property(b => b.Name).HasMaxLength(50);
            modelBuilder.Entity<Project>()
                .Property(b => b.Name).HasMaxLength(50);
            modelBuilder.Entity<Project>()
                .Property(b => b.Description).HasMaxLength(200);
            modelBuilder.Entity<Document>()
                .Property(b => b.Title).HasMaxLength(50);
            modelBuilder.Entity<Document>()
                .Property(b => b.Description).HasMaxLength(200);
            modelBuilder.Entity<ContactInfo>()
                .Property(b => b.Type).HasMaxLength(50);
            modelBuilder.Entity<ContactInfo>()
                .Property(b => b.Value).HasMaxLength(200);

            // indexes
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Name);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email);



            base.OnModelCreating(modelBuilder);
        }
    }
}