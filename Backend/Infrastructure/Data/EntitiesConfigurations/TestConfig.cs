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
    public class TestConfig : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.HasMany(x => x.Questions).WithOne(x => x.Test).HasForeignKey(x => x.TestId);
            builder.Property(t => t.Title)
               .IsRequired()
               .HasMaxLength(200);
        }
    }
}
