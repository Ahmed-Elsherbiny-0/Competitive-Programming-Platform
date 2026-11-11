using Application.Topics.Dtos;
using Domain.Entities;
using Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Topics.Specifications
{
    public  class TopicSpecification:BaseSpecifications<Topic,TopicResponseDto>
    {
        public TopicSpecification(TopicSpecParms parms ):base(x=>
        ((string.IsNullOrEmpty(parms.Search))||(x.Title.ToLower().Contains(parms.Search)))
        )
        {
            AddPagination(parms.PageSize, parms.PageSize * (parms.PageIndex - 1));

            AddInclude(x => x.Tests);
            AddSelect(x => new TopicResponseDto
            (
               x.Content,
                 x.Title,
                 x.Tests.Select(x => x.Title).ToList()
            ));
        }
    }
}
