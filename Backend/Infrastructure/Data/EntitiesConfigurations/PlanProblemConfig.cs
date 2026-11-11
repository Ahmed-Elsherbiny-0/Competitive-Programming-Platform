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
    public class PlanProblemConfig : IEntityTypeConfiguration<PlanProblem>
    {
        public void Configure(EntityTypeBuilder<PlanProblem> b)
        {
            b.HasKey(x => new { x.PlanId, x.ProblemId });

            b.HasOne(x => x.Plan)
             .WithMany(p => p.PlanProblems)
             .HasForeignKey(x => x.PlanId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Problem)
             .WithMany(x=>x.PlanProblems)
             .HasForeignKey(x => x.ProblemId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
