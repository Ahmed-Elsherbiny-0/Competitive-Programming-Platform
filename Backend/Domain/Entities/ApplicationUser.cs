using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Country { get; set; } 
        public IList<RefreshToken> RefreshTokens { get; set; } =[];
        public IList<Vote> Votes { get; set; } =[];
        public Photo Photo { get; set; }
        public IList<Submission> Submissions { get; set; } =[];

        public int? PlanId { get; set; }       
        public Plan? Plan { get; set; }

    }
}
