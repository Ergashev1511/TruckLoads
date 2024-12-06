using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckLoads.Bot.StaticFiles;
using TruckLoads.Services.DTOs;
using TruckLoads.Services.MediatR.Commands.UserCommands;

namespace TruckLoads.Bot.UserServices
{
    public class UserService
    {
        public async Task<bool> UserCreate(UserDto userDto)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Request uchun .
                    var url = $"{StaticFiles.StaticData.Base_Url}/api/User";

                    // Yuboriladigan ma'lumotlar
                    var command = new UserCreateCommand
                    {
                        UserDto = userDto
                    };

                    // JSON'ga o'zgartirish
                    var json = JsonConvert.SerializeObject(command);

                    // Content yaratish
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // HTTP so'rov yuborish
                    var response = await client.PostAsync(url, content);

                    // Javobni tekshirish
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Server Error: {errorContent}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }

    }
}
