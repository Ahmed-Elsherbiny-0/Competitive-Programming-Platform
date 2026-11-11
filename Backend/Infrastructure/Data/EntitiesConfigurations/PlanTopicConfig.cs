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
    public class PlanTopicConfig : IEntityTypeConfiguration<PlanTopic>
    {
        public void Configure(EntityTypeBuilder<PlanTopic> builder)
        {
            builder.HasKey(x => new { x.PlanId, x.TopicId });
            builder .HasOne(x=>x.Plan).WithMany(x=>x.PlanTopics).HasForeignKey(x=>x.PlanId);
            builder.HasOne(x=>x.Topic).WithMany(x=>x.PlanTopics).HasForeignKey(x=>x.TopicId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}