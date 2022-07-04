class Result
{

    /*
     * Complete the 'longestSubarray' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts INTEGER_ARRAY arr as parameter.
     */

    public static int longestSubarray(List<int> arr)
    {
        if (arr.Count < 2)
            return arr.Count;

        int ultimate = 1;
        int ultimateLow = 1;
        int ultimateHigh = 1;

        for (int i = 1; i < arr.Count; i++)
        {
            if (arr[i] == arr[i - 1])
            {
                ultimateLow++;
                ultimateHigh++;

            }
            else if (arr[i] - 1 == arr[i - 1])
            {
                ultimateLow = 1 + ultimateHigh;
                ultimateHigh = 1;

            }
            else if (arr[i] + 1 == arr[i - 1])
            {
                ultimateHigh = 1 + ultimateLow;
                ultimateLow = 1;

            }
            else
            {
                ultimateLow = 1;
                ultimateHigh = 1;
            }

            ultimate = Math.Max(Math.Max(ultimate, ultimateLow), Math.Max(ultimateLow, ultimateHigh));
        }

        return ultimate;

    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int arrCount = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> arr = new List<int>();

        for (int i = 0; i < arrCount; i++)
        {
            int arrItem = Convert.ToInt32(Console.ReadLine().Trim());
            arr.Add(arrItem);
        }

        int result = Result.longestSubarray(arr);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
