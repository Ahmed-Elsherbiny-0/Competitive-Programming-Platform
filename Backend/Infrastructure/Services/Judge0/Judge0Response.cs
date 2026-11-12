using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Services.Judge0
{
    public  class Judge0Response
    {
        [JsonPropertyName("status")] public Judge0Status Status { get; set; } = default!;
        [JsonPropertyName("time")] public string? Time { get; set; }
        [JsonPropertyName("memory")] public int? Memory { get; set; }
        [JsonPropertyName("compile_output")] public string? CompileOutput { get; set; }
    }
    public class Judge0Status { [JsonPropertyName("id")] public int Id { get; set; } }
}
