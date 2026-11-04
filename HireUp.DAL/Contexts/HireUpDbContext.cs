using HireUp.DAL.Models;
using HireUp.DAL.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HireUp.DAL.Contexts
{
    public class HireUpDbContext : IdentityDbContext<ApplicationUser>
    {     
        public HireUpDbContext(DbContextOptions<HireUpDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Technology" },
                new Category { Id = 2, Name = "Design" },
                new Category { Id = 3, Name = "Marketing" },
                new Category { Id = 4, Name = "Business" },
                new Category { Id = 5, Name = "Healthcare" },
                new Category { Id = 6, Name = "Education" },
                new Category { Id = 7, Name = "UnCategorized" }
            );


            builder.Entity<ApplicationUser>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Admin>("Admin")
                .HasValue<Company>("Company")
                .HasValue<JobSeeker>("JobSeeker");


            builder.Entity<Application>()
                .HasOne(a => a.Job)
                .WithMany(j => j.Applications)
                .HasForeignKey(a => a.JobId)
                .OnDelete(DeleteBehavior.Cascade);
        }
  
        public DbSet<Job> Job { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<JobSeeker> JobSeeker { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Application> Application { get; set; }
    }
}
