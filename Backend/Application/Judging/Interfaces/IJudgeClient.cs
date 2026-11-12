using Application.Judging.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Judging.Interfaces
{
    public  interface IJudgeClient
    {
        Task<JudgeResult> RunAsync(JudgeRequest request,CancellationToken ct);
    }
}
