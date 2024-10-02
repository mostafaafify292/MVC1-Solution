﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC1__DAL_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC1__DAL_.Data.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            //fluent APIs
            builder.Property(d => d.id).UseIdentityColumn(10, 10);
            builder.HasMany(D => D.Employees)
                   .WithOne(E => E.department)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Property(d => d.Code)
                   .IsRequired();
            builder.Property(d => d.Name)
                   .IsRequired();
         
        }
    }
}
