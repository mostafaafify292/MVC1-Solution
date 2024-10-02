using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC1__DAL_.Data.Configuration;
using MVC1__DAL_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC1__DAL_.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = ., Database=MVC1, Trusted_Connection=True");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new EmplyeeConfiguration());
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>()
                        .ToTable("Users");
            modelBuilder.Entity<IdentityRole>()
                        .ToTable("Roles");
        }
        public DbSet<Department> departments { get; set; }
        public DbSet<Employee> employees { get; set; }
    }
}
