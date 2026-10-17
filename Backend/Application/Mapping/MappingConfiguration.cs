using Application.Dtos;
using Application.Dtos.Auth;
using Domain.Constants;
using Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class MappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequestDto, ApplicationUser>()
            .Map(y => y.UserName, x => x.Username);

       
 
        }
    }
}
