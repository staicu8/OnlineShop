using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;
using System.Text;
using System.Text.Json;

namespace OnlineShop.Services
{
    public class AIAssistantService : IAIAssistantService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly HttpClient _http = new HttpClient();

        public AIAssistantService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<string> GetProductAnswerAsync(int productId, string question)
        {
            var produs = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.FAQs)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (produs == null)
                return "Produsul nu a fost găsit.";

            var info = $"Produs: {produs.Title}\nCategorie: {produs.Category?.Name}\nPret: {produs.Price} RON\nStoc: {produs.Stock}\nDescriere: {produs.Description}";
            
            if (produs.FAQs != null && produs.FAQs.Any())
            {
                info += "\n\nFAQ:";
                foreach (var faq in produs.FAQs)
                    info += $"\nQ: {faq.Question}\nA: {faq.Answer}";
            }

            return await TrimiteIntrebare(info, question);
        }

        public async Task<string> GetGeneralAnswerAsync(string question)
        {
            var produse = await _context.Products.Where(p => p.Status == ProductStatus.Aprobat).Take(5).ToListAsync();
            var info = "Produse disponibile:\n" + string.Join("\n", produse.Select(p => $"- {p.Title}: {p.Price} RON"));
            return await TrimiteIntrebare(info, question);
        }

        private async Task<string> TrimiteIntrebare(string context, string intrebare)
        {
            var apiKey = _config["GroqAI:ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
                return "AI nu este configurat.";

            var body = new
            {
                model = "llama-3.3-70b-versatile",
                messages = new[] { new { role = "user", content = $"Raspunde in romana, scurt. Context: {context}\n\nIntrebare: {intrebare}" } },
                max_tokens = 150
            };

            try
            {
                _http.DefaultRequestHeaders.Clear();
                _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                
                var resp = await _http.PostAsync("https://api.groq.com/openai/v1/chat/completions",
                    new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));
                
                if (!resp.IsSuccessStatusCode)
                    return "Eroare la AI.";

                var json = await resp.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "Nu am gasit raspuns.";
            }
            catch
            {
                return "Nu am informatii despre asta.";
            }
        }
    }
}
