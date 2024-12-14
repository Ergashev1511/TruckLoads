using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TruckLoads.Bot.StaticFiles;
using TruckLoads.Bot.UserServices;
using TruckLoads.Services.DTOs;

namespace TruckLoads.Bot.ServiceUpdateHandler
{
    public class AdvancedBotUpdateHandler : IUpdateHandler
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(10);

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Error: {exception.Message}");
            return Task.CompletedTask;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update.Message!.Type == MessageType.Text)
            {
                string message = update.Message.Text?.Trim() ?? string.Empty;
                long chatId = update.Message.Chat.Id;

                switch (message)
                {
                    case "/start":
                        await HandleStartCommand(chatId, cancellationToken);
                        break;
                    case "/help":
                        await HandleHelpCommand(chatId, cancellationToken);
                        break;
                    default:
                        break;
                }
            }
            else if (update.Type == UpdateType.CallbackQuery)
            {
                await HandleCallbackQuery(update.CallbackQuery, cancellationToken);
            }
        }

        private async Task HandleStartCommand(long chatId, CancellationToken cancellationToken)
        {
            string json = $@"
                {{
                    ""chat_id"": ""{chatId}"",
                    ""text"": ""Assalomu alaykum! Obuna bo'lish uchun tugmani bosing."",
                    ""reply_markup"": {{
                        ""inline_keyboard"": [
                            [
                                {{ ""text"": ""Obuna bo'lish"", ""callback_data"": ""subscribe"" }}
                            ]
                        ]
                    }}
                }}";

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await SendRequestAsync(content, cancellationToken);
        }

        private async Task HandleHelpCommand(long chatId, CancellationToken cancellationToken)
        {
            string helpMessage = "Salom! Bu botni ishlatish uchun obuna bo'lishingiz kerak. Quyidagi komandalardan foydalanishingiz mumkin:\n\n" +
                                 "/start - Botni ishga tushurish\n" +
                                 "/help - Yordam olish\n" +
                                 "Obuna bo'lish uchun: 'Obuna bo'lish' tugmasini bosing.";

            var content = new StringContent($"{{\"chat_id\": \"{chatId}\", \"text\": \"{helpMessage}\"}}", Encoding.UTF8, "application/json");

            await SendRequestAsync(content, cancellationToken);
        }

        private async Task HandleCallbackQuery(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            string? callbackData = callbackQuery.Data;
            long userId = callbackQuery.From.Id;
            string userName = callbackQuery.From.Username ?? "NoUsername";
            long chatId = callbackQuery.Message.Chat.Id;
            string message = $"Rahmat, @{userName}! Siz obuna bo'ldingiz.";

            if (callbackData == "subscribe")
            {
                var content = new StringContent(
                    $"{{\"chat_id\": \"{chatId}\", \"text\": \"{message}\"}}",
                    Encoding.UTF8,
                    "application/json");

                await SendRequestAsync(content, cancellationToken);

                Console.WriteLine($"Obuna bo'lgan user ID: {userId}, Username: @{userName}");

                // UserDto ma'lumotlarini bazaga saqlash
                UserDto userDto = new UserDto()
                {
                    UserId = userId,
                    UserName = userName ?? "",
                    FirstName = callbackQuery.From.FirstName ?? "",
                    LastName = callbackQuery.From.LastName ?? ""

                };

                // Bazaga qo'shish (Bu yerda konkret kodni yozing)
                await SaveUserToDatabase(userDto);
            }
        }

        private async Task SendRequestAsync(StringContent content, CancellationToken cancellationToken)
        {
            await _semaphore.WaitAsync(cancellationToken); // Limit concurrent requests

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync(StaticFiles.StaticData.url, content, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Xatolik yuz berdi: {response.StatusCode}\n{error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending request: {ex.Message}");
            }
            finally
            {
                _semaphore.Release(); // Release semaphore to allow the next request
            }
        }

        private async Task SaveUserToDatabase(UserDto userDto)
        {
            Console.WriteLine($"User {userDto.UserName} saved to database.");
            var service = new UserService();
            
            await service.UserCreate(userDto);
        }
    }
}
