using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(new[] {"12:12:12"}, new [] { "few seconds ago" }, "12:12:12");
            Test(new[] { "23:23:23", "23:23:23" }, new[] { "59 minutes ago", "59 minutes ago" }, "00:22:23");
            Test(new[] { "00:10:10", "00:10:10" }, new[] { "59 minutes ago", "1 hours ago" }, "impossible");
            Test(new[] { "11:59:13", "11:13:23", "12:25:15" }, new[] { "few seconds ago", "46 minutes ago", "23 hours ago" }, "11:59:23");
            Console.ReadKey(true);
        }

        private static void Test(string[] postTimes, string[] showTimes, string expected)
        {
            var result = GetCurrentTime(postTimes, showTimes).Equals(expected) ? "PASS" : "FAIL";
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            for (int i = 0; i < exactPostTime.Length; i++) 
            {
                for (int j = i + 1; j < exactPostTime.Length; j++) 
                {
                    if (exactPostTime[i] == exactPostTime[j])
                    {
                        if (showPostTime[i] != showPostTime[j])
                            return "impossible";
                    }
                        
                }
            }

            string[] currentTime = new string[exactPostTime.Length];

            for(int i = 0; i<exactPostTime.Length; i++) 
            {
                string[] hour_min_sec = exactPostTime[i].Split(":");
                DateTime dateTime = new DateTime(1996, 8, 1, Int32.Parse(hour_min_sec[0]), Int32.Parse(hour_min_sec[1]), Int32.Parse(hour_min_sec[2]));

                if(showPostTime[i].Contains("seconds")) 
                {
                    currentTime[i] = exactPostTime[i];
                } 
                else if(showPostTime[i].Contains("minutes")) 
                {
                    string minutes = showPostTime[i].Split(" ")[0];
                    TimeSpan tspan = new TimeSpan(0, Int32.Parse(minutes), 0);
                    currentTime[i] = dateTime.Add(tspan).ToString().Split(" ")[1];
                } 
                else if(showPostTime[i].Contains("hours")) 
                {
                    string hours = showPostTime[i].Split(" ")[0];
                    TimeSpan tspan = new TimeSpan(Int32.Parse(hours), 0, 0);
                    currentTime[i] = dateTime.Add(tspan).ToString().Split(" ")[1];
                }
            }
            Array.Sort(currentTime);
            return currentTime[(exactPostTime.Length-1)];

            throw new NotImplementedException();
        }
    }
}