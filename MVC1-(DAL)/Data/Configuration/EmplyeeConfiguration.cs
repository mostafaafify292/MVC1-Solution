using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC1__DAL_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC1__DAL_.Data.Configuration
{
    public class EmplyeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(p => p.Salary)
                    .HasColumnType("decimal(18,2)");
            builder.Property(p => p.Gender)
                   .HasConversion
                   (
                   (Gender) => (Gender).ToString(),
                   (genderAsString) => (Gender)Enum.Parse(typeof(Gender), genderAsString, true)
                   );
            builder.Property(p => p.Name)
                   .IsRequired(true)
                   .HasMaxLength(50);
           
        }
    }
}
