using System.Text.Json;

class Result
{

    /*
     * Complete the 'getNumDraws' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts INTEGER year as parameter.
     */

    public static int getNumDraws(int year)
    {
        using HttpClient client = new();
        try
        {
            var drawCount = 0;

            for (int i = 0; i <= 10; i++)
            {

                string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1goals={i}&team2goals={i}";

                var response = client.GetAsync(url).Result;
                string responseData = response.Content.ReadAsStringAsync().Result;
                var dataObject = JsonSerializer.Deserialize<ResponseObject>(responseData);

                drawCount = drawCount + dataObject?.total ?? 0;
            }

            return drawCount;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return 0;
    }
}

public class Data
{
    public string competition { get; set; }
    public int year { get; set; }
    public string round { get; set; }
    public string team1 { get; set; }
    public string team2 { get; set; }
    public string team1goals { get; set; }
    public string team2goals { get; set; }
}

public class ResponseObject
{
    public int page { get; set; }
    public int per_page { get; set; }
    public int total { get; set; }
    public int total_pages { get; set; }
    public List<Data> data { get; set; }
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(Environment.GetEnvironmentVariable("OUTPUT_PATH") ?? "", true);


        int year = Convert.ToInt32(Console.ReadLine()?.Trim());

        int result = Result.getNumDraws(year);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
