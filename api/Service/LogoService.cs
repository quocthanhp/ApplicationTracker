using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using api.Interfaces;
using System.Text.Json;
using api.Dtos.Application;

namespace api.Service
{
    public class LogoService : ILogoService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public LogoService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<string> GetLogoAsync(string companyName)
        {
            try
            {   
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config["ConnectionStrings:LogoApiKey"]);
                var result = await _httpClient.GetAsync($"https://api.logo.dev/search?q={companyName}");

                if (result.IsSuccessStatusCode) 
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var tasks = JsonSerializer.Deserialize<Logo[]>(content);
                    var logo = tasks[0];
                    if (logo != null) 
                    {
                        return logo.logo_url;
                    } 
                    else {
                        return null;
                    }
                }
                return null;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
    
}