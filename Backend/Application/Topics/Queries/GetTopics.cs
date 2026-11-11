using Application.Comman;
using Application.Topics.Dtos;
using Application.Topics.Specifications;
using Domain.Comman;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Topics.Queries
{
    public  record GetTopicsQuery(TopicSpecParms parms) :IRequest<Result<Pagination<TopicResponseDto>>>;
    public class GetTopicsQueryHandler(IGenericRepository<Topic>_topicRepo) : IRequestHandler<GetTopicsQuery, Result<Pagination<TopicResponseDto>>>
    {
        public async Task<Result<Pagination<TopicResponseDto>>> Handle(GetTopicsQuery request, CancellationToken cancellationToken)
        {
            var spec = new TopicSpecification(request.parms);
           var items=await  _topicRepo.ListAsync(spec);
           var count=await _topicRepo.CountAsync(spec);
            var page = new Pagination<TopicResponseDto>(request.parms.PageSize, request.parms.PageIndex, count, items);
            return Result.Success(page);
        }
    }
}
