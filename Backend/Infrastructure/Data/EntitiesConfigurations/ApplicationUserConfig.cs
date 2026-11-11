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
    public  class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.UserName).IsRequired();
            builder.HasMany(x => x.RefreshTokens).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Submissions).WithOne(x=>x.ApplicationUser).HasForeignKey(x=>x.ApplicationUserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(u => u.Plan).WithMany(p => p.Users).HasForeignKey(u => u.PlanId).OnDelete(DeleteBehavior.SetNull);

        }
    }
}
