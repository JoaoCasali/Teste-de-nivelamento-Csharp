using Questao2;
using System.Text.Json;

public class Program
{
    private static readonly HttpClient _http = new();

    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        int pagina = 1;
        int totalGoals = 0;
        int quantidadeConvertida;
        string resposta;
        MatchesResponse? dto;
        JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
        while (true)
        {
            resposta = _http.GetStringAsync($"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}&page={pagina}").ConfigureAwait(false).GetAwaiter().GetResult();

            if (string.IsNullOrEmpty(resposta))
            {
                return 0;
            }


            dto = JsonSerializer.Deserialize<MatchesResponse?>(resposta, options);
            if (dto == null)
            {
                return 0;
            }

            if (dto.Data == null || dto.Data.Count <= 0)
            {
                break;
            }

            foreach (MatchDto item in dto.Data)
            {
                if (int.TryParse(item.Team1Goals, out quantidadeConvertida))
                {
                    totalGoals += quantidadeConvertida;
                }
            }

            if (dto.TotalPages == pagina)
            {
                break;
            }

            pagina++;
        }

        pagina = 1;

        while (true)
        {
            resposta = _http.GetStringAsync($"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team2={team}&page={pagina}").ConfigureAwait(false).GetAwaiter().GetResult();

            if (string.IsNullOrEmpty(resposta))
            {
                return 0;
            }


            dto = JsonSerializer.Deserialize<MatchesResponse?>(resposta, options);
            if (dto == null)
            {
                return 0;
            }

            if (dto.Data == null || dto.Data.Count <= 0)
            {
                break;
            }

            foreach (MatchDto item in dto.Data)
            {
                if (int.TryParse(item.Team2Goals, out quantidadeConvertida))
                {
                    totalGoals += quantidadeConvertida;
                }
            }

            if (dto.TotalPages == pagina)
            {
                break;
            }

            pagina++;
        }

        return totalGoals;
    }

}