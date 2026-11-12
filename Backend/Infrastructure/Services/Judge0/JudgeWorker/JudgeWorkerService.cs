using Application.Judging.Dtos;
using Application.Judging.Interfaces;
using Domain.Comman;
using Domain.Entities;
using Domain.Enums;
using Hangfire;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Judge0.JudgeWorker
{
    public class JudgeWorkerService(IServiceScopeFactory factory, ILogger<JudgeWorkerService> _logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stop)
        {
            while (!stop.IsCancellationRequested)
            {
                try
                {
                    using var scope = factory.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var judge = scope.ServiceProvider.GetRequiredService<IJudgeClient>();
                    var sub = await db.Set<Submission>()
                                .Include(s => s.Problem).ThenInclude(p => p.TestCases)
                                .Where(s => s.Verdict == Verdict.Pending)
                                .OrderBy(s => s.Id)
                                .FirstOrDefaultAsync(stop);
                    if (sub is null) { await Task.Delay(1000, stop); continue; }

                    sub.Verdict = Verdict.Running;
                    await db.SaveChangesAsync(stop);

                    await JudgeOne(sub, judge, stop);
                    await db.SaveChangesAsync(stop);
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex, "judge loop failed");
                    await Task.Delay(2000, stop);
                }

            }
        }
        private static async Task JudgeOne(Submission sub, IJudgeClient judge, CancellationToken ct)
        {
            int maxTime = 0, maxMem = 0;

            foreach (var t in sub.Problem.TestCases.OrderBy(t => t.SortOrder))
            {
                var r = await judge.RunAsync(new JudgeRequest(
                    sub.SourceCode, sub.Language, t.Input, t.ExpectedOutput,
                    sub.Problem.TimeLimitMs, sub.Problem.MemoryLimitMb), ct);

                maxTime = Math.Max(maxTime, r.TimeMs ?? 0);
                maxMem = Math.Max(maxMem, r.MemoryKb ?? 0);

                if (r.Verdict != Verdict.Accepted)
                {
                    sub.Verdict = r.Verdict;
                    sub.FailedTestCaseOrder = t.SortOrder;
                    sub.CompilerOutput = Truncate(r.CompilerOutput, 4000);
                    sub.TimeMs = maxTime;
                    sub.MemoryKb = maxMem;
                    return;
                }
            }

            sub.Verdict = Verdict.Accepted;
            sub.TimeMs = maxTime;
            sub.MemoryKb = maxMem;
        }

        private static string? Truncate(string? s, int n) =>
            s is null ? null : s.Length <= n ? s : s[..n];
    }
}
