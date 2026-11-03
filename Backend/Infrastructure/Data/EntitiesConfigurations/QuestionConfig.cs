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
    internal class QuestionConfig : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasMany(x=>x.Answers).WithOne(x => x.Question).HasForeignKey(x=> x.QuestionId);
            builder.Property(q => q.Content)
                .IsRequired()
                .HasMaxLength(1000);
        }
    }
}
