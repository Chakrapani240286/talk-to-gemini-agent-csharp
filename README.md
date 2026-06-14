# talk-to-gemini-agent-csharp

A beginner-friendly C# console application that demonstrates how to build a simple AI agent using Google Gemini API, HttpClient, async/await, and Newtonsoft.Json.

## 🚀 Features

- Ask questions from the console.
- Uses Google Gemini 2.5 Flash.
- Sends requests using `HttpClient`.
- Uses `Newtonsoft.Json` for JSON serialization and deserialization.
- Runs continuously until the user types `exit`.
- Simple and easy to understand.

## 🛠 Technologies Used

- C#
- .NET 8+
- Google Gemini API
- HttpClient
- Async/Await
- Newtonsoft.Json

## 📂 Project Structure

```text
talk-to-gemini-agent-csharp
│
├── Program.cs
└── README.md
```

## 📦 Prerequisites

- .NET 8 or later
- Visual Studio 2022 or VS Code
- Google Gemini API Key

Install Newtonsoft.Json:

```bash
dotnet add package Newtonsoft.Json
```

## 🔑 Configure API Key

Replace:

```csharp
string apiKey = "";
```

with:

```csharp
string apiKey = "YOUR_API_KEY";
```

## ▶️ Run the Application

```bash
dotnet run
```

## 💻 Source Code

```csharp
using Newtonsoft.Json;
using System.Text;

Console.WriteLine("=== My First AI Agent (Gemini) ===");

string apiKey = "";

while (true)
{
    Console.Write("\nAsk Question (type exit to quit): ");

    string? question = Console.ReadLine();

    if (string.Equals(
        question,
        "exit",
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
        await client.PostAsync(
            url,
            content);

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
```

## 💻 Example Session

```text
=== My First AI Agent (Gemini) ===

Ask Question (type exit to quit): What is AWS Lambda?

Agent Response:
AWS Lambda is a serverless computing service provided by Amazon Web Services that allows you to run code without managing servers.

Ask Question (type exit to quit): Who invented C#?

Agent Response:
C# was created by Microsoft and led by Anders Hejlsberg.

Ask Question (type exit to quit): exit
```

## 📚 Topics Covered

- AI Agents
- Gemini API
- HTTP APIs
- JSON Serialization
- Async Programming
- C# Console Applications

## 🔮 Future Enhancements

- Conversation memory
- SQL Server integration
- Function calling
- Tool calling
- Streaming responses
- File upload support
- Web search
- RAG (Retrieval-Augmented Generation)
- Multi-agent architecture

## 📄 License

This project is intended for learning and educational purposes.
