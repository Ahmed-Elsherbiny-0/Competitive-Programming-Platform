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
    public class VoteConfig : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasOne(x=>x.ApplicationUser).WithMany(x=>x.Votes).HasForeignKey(x=>x.ApplicationUserId);
            builder.Property(v => v.ApplicationUserId)
           .IsRequired();
            builder.HasIndex(v => new { v.ApplicationUserId, v.TestId })
                .IsUnique();

            builder.HasOne(v => v.Test)
                .WithMany()
                .HasForeignKey(v => v.TestId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(v => v.VoteAnswers)
               .WithOne(va => va.Vote)
               .HasForeignKey(va => va.VoteId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
