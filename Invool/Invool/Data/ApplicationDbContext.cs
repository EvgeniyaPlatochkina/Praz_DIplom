using Invool.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invool.Data
{
    public  class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        private const string connectionString = @"Server=localhost\SQLEXPRESS; Database=Praz; Trusted_Connection=true; TrustServerCertificate=true;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        public DbSet<Thing> Things { get; set; }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<ThingCategorie> ThingCategories { get; set; }
        public DbSet<RecordSchool> RecordSchools { get; set; }
        public DbSet<Responsible> Responsibles { get; set; }
        public DbSet<Location> Locations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
