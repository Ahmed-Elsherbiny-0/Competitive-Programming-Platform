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
    internal class TopicConfig : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.HasMany(x => x.Tests).WithOne(x=>x.Topic).HasForeignKey(x => x.TopicId);
            builder.Property(t => t.Title)
               .IsRequired()
               .HasMaxLength(400);
            builder.Property(t => t.Content)
                .IsRequired();
        }
    }
}
