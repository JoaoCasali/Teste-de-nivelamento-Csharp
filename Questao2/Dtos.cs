using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Questao2
{
    public record MatchDto
    {
        [JsonPropertyName("competition")]
        public string Competition { get; init; } = null!;

        [JsonPropertyName("year")]
        public int Year { get; init; }

        [JsonPropertyName("round")]
        public string Round { get; init; } = null!;

        [JsonPropertyName("team1")]
        public string Team1 { get; init; } = null!;

        [JsonPropertyName("team2")]
        public string Team2 { get; init; } = null!;

        [JsonPropertyName("team1goals")]
        public string Team1Goals { get; init; } = null!;

        [JsonPropertyName("team2goals")]
        public string Team2Goals { get; init; } = null!;
    }

    public record MatchesResponse
    {
        [JsonPropertyName("page")]
        public int Page { get; init; }

        [JsonPropertyName("per_page")]
        public int PerPage { get; init; }

        [JsonPropertyName("total")]
        public int Total { get; init; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; init; }

        [JsonPropertyName("data")]
        public IReadOnlyList<MatchDto> Data { get; init; } = Array.Empty<MatchDto>();
    }
}
