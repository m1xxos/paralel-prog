using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;

class Program
{
    // Метод для асинхронного получения содержимого сайта
    public static async Task<string> FetchSiteAsync(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }

    // Метод для подсчета количества вхождений тега <div> в HTML-странице
    public static int CountDivTags(string htmlContent)
    {
        // Используем регулярное выражение для поиска всех тегов <div>
        Regex regex = new Regex("<div[^>]*>");
        MatchCollection matches = regex.Matches(htmlContent);
        return matches.Count;
    }

    // Метод для последовательного опроса сайтов и подсчета тегов <div>
    public static async Task SequentialRequestsAsync(string[] urls)
    {
        int totalDivCount = 0;

        foreach (var url in urls)
        {
            string result = await FetchSiteAsync(url);
            int divCount = CountDivTags(result);
            totalDivCount += divCount;
            Console.WriteLine($"На сайте {url} найдено {divCount} тегов <div>.");
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

        // Засекаем время выполнения
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Запуск асинхронного запроса и подсчета тегов <div>
        await SequentialRequestsAsync(urls);

        stopwatch.Stop();
        Console.WriteLine($"Время выполнения запросов: {stopwatch.ElapsedMilliseconds} мс");
    }
}
