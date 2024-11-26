using Azure;
using Azure.AI.OpenAI;
using Azure.Identity;
using OpenAI.Chat;
using api.Helpers;
using api.Interfaces;

namespace api.Service
{
    public class KeyPhaseExtractionService : IKeyPhaseExtraction
    {
        private readonly IConfiguration _config;

        public KeyPhaseExtractionService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<List<string>> GetKeyPhaseAsync(string jobDescription)
        {
            if (string.IsNullOrEmpty(jobDescription))
            {
                return [];
            }

            try
            {   
                string endpoint = "https://quoct-m3yck60w-swedencentral.openai.azure.com/";
                string key = _config["ConnectionStrings:AzureAIApiKey"];
                AzureKeyCredential credential = new AzureKeyCredential(key);
                AzureOpenAIClient azureClient = new(new Uri(endpoint), credential);
                ChatClient chatClient = azureClient.GetChatClient("gpt-4-32k");

                ChatCompletion completion = chatClient.CompleteChat(
                    new ChatMessage[] {
                        new SystemChatMessage("You are an AI assistant that extract soft skills and hard skills as keywords (1 to 4 in length) from job description and returns result in a list like [a, b, c]."),
                        new UserChatMessage(jobDescription)
                    }
                );

                Console.Write(completion.Content[0].Text);

                return StringListConverter.ConvertStringToList(completion.Content[0].Text);
            }
            catch (Exception ex)
            {
                return [];
            }
        }
    }
    
}