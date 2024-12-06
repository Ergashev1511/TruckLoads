using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TruckLoads.Bot.ServiceUpdateHandler;
using TruckLoads.Bot.StaticFiles;
namespace TelegramBotExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var botClient = new TelegramBotClient(StaticData.botToken);


            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            var cts = new CancellationTokenSource();

            Console.WriteLine("Starting bot...");

            var updateHandler = new AdvancedBotUpdateHandler();

            botClient.StartReceiving(
            updateHandler: updateHandler,
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token
        );

            Console.WriteLine("Bot is running. Press any key to exit.");
            Console.ReadKey();
            cts.Cancel();
        }





    }
}
