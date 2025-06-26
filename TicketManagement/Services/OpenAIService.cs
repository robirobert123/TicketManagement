using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace TicketManagement.Services
{
    public class OpenAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenAIService(IConfiguration configuration)
        {
            _apiKey = configuration["OpenAI:ApiKey"];
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.openai.com/");
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);
            _httpClient.DefaultRequestHeaders.Add("OpenAI-Beta", "assistants=v2");

        }
        public async Task<string> GenerateTicketAsync(string prompt)
        {
            var requestBody = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                        new { role = "system", content = "Esti un generator de tickete helpdesk. Returneaza un obiect JSON cu campurile: Title, Description, PriorityName. Nu adauga alt text explicativ." },
                        new { role = "user", content = prompt }
                    }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("v1/chat/completions", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Eroare completă OpenAI: " + errorContent);

                throw new Exception($"OpenAI API call failed with status code {response.StatusCode}");
            }
            var result = await response.Content.ReadAsStringAsync();
            var jsonResonse = JsonDocument.Parse(result);
            return jsonResonse.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
        }
    }
}

