using System;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Models
{
    public sealed class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        
        public DatabaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Data");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Александр",
                LastName = "Секретный",
                Login = "alexsecret",
                Password = "qpwoeiruty",
                MiddleName = "Сергеевич"
            });
        }
    }
}