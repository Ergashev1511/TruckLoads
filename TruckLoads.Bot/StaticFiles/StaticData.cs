using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckLoads.Bot.StaticFiles
{
    public static class StaticData
    {
        internal static string botToken = "7741716796:AAF9N4BJPXFb27Ud6YRVjKKZeZIe1Jv6JbY";

        public static string url = $"https://api.telegram.org/bot{botToken}/sendMessage";

        public static string Base_Url = $"https://localhost:7206";
    }
}
