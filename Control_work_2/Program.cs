using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;

class Program
{
    
    public static async Task<string> FetchSiteAsync(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }

    
    public static int CountDivTags(string htmlContent)
    {
        Regex regex = new Regex("<div[^>]*>");
        MatchCollection matches = regex.Matches(htmlContent);
        return matches.Count;
    }

    
    public static async Task ParallelRequestsAsync(string[] urls)
    {
        
        var fetchTasks = new Task<string>[urls.Length];
        for (int i = 0; i < urls.Length; i++)
        {
            fetchTasks[i] = FetchSiteAsync(urls[i]);
        }

        
        var results = await Task.WhenAll(fetchTasks);

        int totalDivCount = 0;

        for (int i = 0; i < results.Length; i++)
        {
            int divCount = CountDivTags(results[i]);
            totalDivCount += divCount;
            Console.WriteLine($"На сайте {urls[i]} найдено {divCount} тегов <div>.");
        }

        Console.WriteLine($"Общее количество тегов <div> на всех сайтах: {totalDivCount}");
    }

    static async Task Main(string[] args)
    {
        string[] urls = new string[]
        {
            "https://github.com/ansible/ansible",
            "https://github.com/kubernetes/kubernetes",
            "https://github.com/python/cpython",
            "https://github.com/torvalds/linux",
            "https://github.com/moby/moby"
        };

        
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        
        await ParallelRequestsAsync(urls);

        stopwatch.Stop();
        Console.WriteLine($"Время выполнения запросов: {stopwatch.ElapsedMilliseconds} мс");
    }
}
