using Newtonsoft.Json;
using System.Text;

Console.WriteLine("=== My First AI Agent (Gemini) ===");

string apiKey = "";

while (true)
{
    Console.Write("\nAsk Question (type exit to quit): ");

    string? question = Console.ReadLine();

    if (string.Equals(question, "exit",
        StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    string answer =
        await AskAgent(question!, apiKey);

    Console.WriteLine("\nAgent Response:");
    Console.WriteLine(answer);
}

static async Task<string> AskAgent(
    string question,
    string apiKey)
{
    using HttpClient client = new();

    string url =
        $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";

    var requestBody = new
    {
        contents = new[]
        {
            new
            {
                parts = new[]
                {
                    new
                    {
                        text = question
                    }
                }
            }
        }
    };

    string json =
        JsonConvert.SerializeObject(requestBody);

    HttpContent content =
        new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

    HttpResponseMessage response =
        await client.PostAsync(url, content);

    string result =
        await response.Content.ReadAsStringAsync();

    dynamic? data =
        JsonConvert.DeserializeObject(result);

    try
    {
        return data.candidates[0]
                   .content
                   .parts[0]
                   .text
                   .ToString();
    }
    catch
    {
        return result;
    }
}