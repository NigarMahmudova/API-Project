using EducationApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationApi.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Point).HasColumnType("decimal(4,2)");
            builder.HasOne(x=>x.Group).WithMany(x=>x.Students).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
