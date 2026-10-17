using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specifications
{
    public class UserSpecification:BaseSpecifications<ApplicationUser>
    {
        public UserSpecification(string email ):base(x=>x.Email==email)
        {
            AddInclude(x => x.Photo);
            AddInclude(x => x.RefreshTokens);
        }
    }
}
