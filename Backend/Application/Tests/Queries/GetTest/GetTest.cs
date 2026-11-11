using Application.Tests.Dtos;
using Application.Tests.Specification;
using Domain.Comman;
using Domain.Entities;
using Domain.Interfaces;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Queries.GetTest
{
    public  record GetTestQuery(int Id):IRequest<Result<TestDto>>;
    public class GetTestQueryHandler(IGenericRepository<Test>_testRepo) : IRequestHandler<GetTestQuery, Result<TestDto>>
    {
        public async Task<Result<TestDto>> Handle(GetTestQuery query, CancellationToken cancellationToken)
        {
            var spec =await  _testRepo.GetEntityWithspec(new TestSpecification(query.Id));
          
           var dto= spec.Adapt<TestDto>();
            return Result.Success(dto);
        }
    }
}
