using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace TruckLoads.Bot.StaticFiles
{
    public static class StaticData
    {
        private static readonly IConfiguration _configuration;

        static StaticData()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory) // Ishchi katalogni sozlash
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // JSON faylni yuklash
                .Build();


            botToken = _configuration["BotSettings:Token"] ?? "DefaultToken";
            url = $"https://api.telegram.org/bot{botToken}/sendMessage";
            Base_Url = "https://localhost:7206";
        }

        public static string botToken { get; private set; }

        public static string url { get; private set; }

        public static string Base_Url { get; private set; }
    }
}
