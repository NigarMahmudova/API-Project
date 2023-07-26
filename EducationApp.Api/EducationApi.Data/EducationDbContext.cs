using EducationApi.Data.Configurations;
using EducationApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationApi.Data
{
    public class EducationDbContext:DbContext
    {
        public EducationDbContext(DbContextOptions<EducationDbContext> options):base(options) { }
        
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GroupConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
