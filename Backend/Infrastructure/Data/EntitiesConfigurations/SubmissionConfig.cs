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
    public class SubmissionConfig : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> b)
        {
            b.Property(x => x.SourceCode).IsRequired().HasMaxLength(65536);
            b.Property(x => x.CompilerOutput).HasMaxLength(4000);
            b.Property(x => x.ApplicationUserId).IsRequired().HasMaxLength(450);

            b.HasOne(x => x.Problem)
             .WithMany(p => p.Submissions)
             .HasForeignKey(x => x.ProblemId)
             .OnDelete(DeleteBehavior.Restrict);

            
            b.HasIndex(x => new { x.ApplicationUserId, x.ProblemId });

            
            b.HasIndex(x => x.Verdict);

           
            b.HasIndex(x => x.CreatedAt);
        }
    }
}
