using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.EntitiesConfigurations
{
    public  class PlanConfig:IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> b)
        {
            b.Property(x => x.Title).IsRequired().HasMaxLength(100);
            b.Property(x => x.Description).HasMaxLength(500);
            b.HasIndex(x => x.Level).IsUnique();
        }
    }
}
