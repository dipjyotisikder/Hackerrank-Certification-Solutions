using System.Text.Json;

class Result
{

    /*
     * Complete the 'getWinnerTotalGoals' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. STRING competition
     *  2. INTEGER year
     */

    public static int getWinnerTotalGoals(string competition, int year)
    {
        using HttpClient client = new();

        string url = $"https://jsonmock.hackerrank.com/api/football_competitions?name={competition}&year={year}";

        var response1 = client.GetAsync(url).Result;
        string responseData = response1.Content.ReadAsStringAsync().Result;
        var competitionObject = JsonSerializer.Deserialize<Competition>(responseData);
        var winner = competitionObject?.data.First().winner ?? string.Empty;

        var team1Goals = GetTotalGoalsOfTeam1(client, competition, year, winner);
        var team2Goals = GetTotalGoalsOfTeam2(client, competition, year, winner);

        return team1Goals + team2Goals;
    }

    private static int GetTotalGoalsOfTeam1(HttpClient client, string competition, int year, string? winner)
    {
        string goalsUrl = $"https://jsonmock.hackerrank.com/api/football_matches?competition={competition}&year={year}&team1={winner}&page=1";
        var goalsResponse = client.GetAsync(goalsUrl).Result;
        string goalsResponseData = goalsResponse.Content.ReadAsStringAsync().Result;
        var goalsObject = JsonSerializer.Deserialize<Goals>(goalsResponseData);

        var sum = goalsObject?.data.Select(x => int.Parse(x.team1goals)).Sum() ?? 0;

        if (goalsObject != null && goalsObject.total_pages > 1)
        {
            for (var i = 2; i <= goalsObject.total_pages; i++)
            {
                string url = $"https://jsonmock.hackerrank.com/api/football_matches?competition={competition}&year={year}&team1={winner}&page={i}";
                var res = client.GetAsync(url).Result;
                string data = res.Content.ReadAsStringAsync().Result;
                var obj = JsonSerializer.Deserialize<Goals>(data);
                sum = sum + obj?.data.Select(x => int.Parse(x.team1goals)).Sum() ?? 0;
            }
        }

        return sum;
    }

    private static int GetTotalGoalsOfTeam2(HttpClient client, string competition, int year, string? winner)
    {
        string goalsUrl = $"https://jsonmock.hackerrank.com/api/football_matches?competition={competition}&year={year}&team2={winner}&page=1";
        var goalsResponse = client.GetAsync(goalsUrl).Result;
        string goalsResponseData = goalsResponse.Content.ReadAsStringAsync().Result;
        var goalsObject = JsonSerializer.Deserialize<Goals>(goalsResponseData);

        var sum = goalsObject?.data.Select(x => int.Parse(x.team2goals)).Sum() ?? 0;

        if (goalsObject != null && goalsObject.total_pages > 1)
        {
            for (var i = 2; i <= goalsObject.total_pages; i++)
            {
                string url = $"https://jsonmock.hackerrank.com/api/football_matches?competition={competition}&year={year}&team2={winner}&page={i}";
                var res = client.GetAsync(url).Result;
                string data = res.Content.ReadAsStringAsync().Result;
                var obj = JsonSerializer.Deserialize<Goals>(data);
                sum = sum + obj?.data.Select(x => int.Parse(x.team2goals)).Sum() ?? 0;
            }
        }

        return sum;

    }
}

public class Competition
{
    public int page { get; set; }
    public int per_page { get; set; }
    public int total { get; set; }
    public int total_pages { get; set; }
    public List<CompetitionDatum> data { get; set; }
}

public class CompetitionDatum
{
    public string name { get; set; }
    public string country { get; set; }
    public int year { get; set; }
    public string winner { get; set; }
    public string runnerup { get; set; }
}


public class Goals
{
    public int page { get; set; }
    public int per_page { get; set; }
    public int total { get; set; }
    public int total_pages { get; set; }
    public List<GoalsDatum> data { get; set; }
}

public class GoalsDatum
{
    public string competition { get; set; }
    public int year { get; set; }
    public string round { get; set; }
    public string team1 { get; set; }
    public string team2 { get; set; }
    public string team1goals { get; set; }
    public string team2goals { get; set; }
}


class Solution
{
    public static void Main(string[] args)
    {
        /*TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);
*/
        /*string competition = Console.ReadLine();

        int year = Convert.ToInt32(Console.ReadLine().Trim());*/

        var competition = "English Premier League";
        var year = 2011;

        int result = Result.getWinnerTotalGoals(competition, year);

        /* textWriter.WriteLine(result);

         textWriter.Flush();
         textWriter.Close();*/
    }
}
