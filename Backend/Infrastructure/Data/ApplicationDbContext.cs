using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Vote>Votes { get; set; }
        public DbSet<Topic>Topics { get; set; }
        public DbSet<Question>Questions { get; set; }
        public DbSet<Test>Tests { get; set; }
        public DbSet<Answer>Answers { get; set; }
        public DbSet<VoteAnswer>VoteAnswers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
