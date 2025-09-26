using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AkelTestingTool.Models;
using System.Reflection.Emit;
using WingtipToys.Models;

namespace AkelTestingTool.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        //public DbSet<AkelTestingTool.Models.Projects> Projects { get; set; }

        // public DbSet<AkelTestingTool.Models.BugsSummary> BugsSummary { get; set; }

        public DbSet<Projects> Projects { get; set; }
        public DbSet<BugsSummary> BugsSummary { get; set; }
        public DbSet<ProjectTests> ProjectTests { get; set; }
        public DbSet<TestCases> TestCases { get; set; }
        public DbSet<TestExcutions> TestExcutions { get; set; }
        public DbSet<TestsExeResults> TestsExeResults { get; set; }

        public DbSet<Status> Status { get; set; }
        public DbSet<StatusTE> StatusTE { get; set; }
        public DbSet<StatusTER> StatusTER { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<AssignedTo> AssignedTo { get; set; }

    }
}
