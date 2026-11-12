using Application.Judging.Dtos;
using Application.Judging.Interfaces;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Infrastructure.Services.Judge0
{
    public class Judge0Client(HttpClient _http) : IJudgeClient
    {
        public async Task<JudgeResult> RunAsync(JudgeRequest request,CancellationToken ct )
        {
            var body = new
            {
                source_code = request.SourceCode,
                language_id = Judge0Languages.ToId(request.Language),
                stdin = request.Input,
                expected_output = request.ExpectedOutput,
                cpu_time_limit = request.TimeLimitMs / 1000.0,
                memory_limit = request.MemoryLimitMb * 1024   // Judge0 uses KB
            };
            var content = new StringContent(
               JsonSerializer.Serialize( body), Encoding.UTF8, "application/json");
            var res = await _http.PostAsync("submissions?base64_encoded=false&wait=true", content, ct);

            //res.EnsureSuccessStatusCode();

            var j = await res.Content.ReadFromJsonAsync<Judge0Response>(cancellationToken: ct)
               ?? throw new InvalidOperationException("Empty Judge0 response");

            var verdict = j.Status.Id switch
            {
                3 => Verdict.Accepted,
                4 => Verdict.WrongAnswer,
                5 => Verdict.TimeLimitExceeded,
                6 => Verdict.CompilationError,
                >= 7 and <= 12 => Verdict.RuntimeError,
                _ => Verdict.InternalError
            };
            int? timeMs = double.TryParse(j.Time, CultureInfo.InvariantCulture, out var s)
           ? (int)(s * 1000) : null;

            return new JudgeResult(verdict, timeMs, j.Memory, j.CompileOutput);
        }

    }
}
