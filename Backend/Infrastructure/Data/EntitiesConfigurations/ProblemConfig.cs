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
    public class ProblemConfig : IEntityTypeConfiguration<Problem>
    {
        public void Configure(EntityTypeBuilder<Problem> b)
        {
        b.Property(x => x.Title).IsRequired().HasMaxLength(200);
            b.HasIndex(x => x.Title).IsUnique();
        b.Property(x => x.Statement).IsRequired();
        b.Property(x => x.InputFormat).IsRequired();
        b.Property(x => x.OutputFormat).IsRequired();
        b.Property(x => x.Constraints).HasMaxLength(1000);
            b.HasMany(x => x.TestCases)
   .WithOne(t => t.Problem)
   .HasForeignKey(t => t.ProblemId);

            b.HasOne(x => x.Topic)
        .WithMany(t => t.Problems)
        .HasForeignKey(x => x.TopicId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
