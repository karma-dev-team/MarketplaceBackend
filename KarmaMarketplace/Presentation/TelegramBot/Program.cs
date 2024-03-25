using SKitLs.Bots.Telegram.Core.Building;
using SKitLs.Bots.Telegram.Core.Interactions.Defaults;
using SKitLs.Bots.Telegram.Core.UpdatesCasting.Signed;
using Telegram.Bot;

namespace KarmaMarketplace.Presentation.TelegramBot
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var privateMessages = new DefaultMessageUpdateHandler();
            var privateTexts = new Defa
            {
                CommandsManager = new DefaultActionManager<SignedMessageTextUpdate>()
            };
            privateTexts.CommandsManager.AddSafely(StartCommand);
            privateMessages.TextMessageUpdateHandler = privateTexts;

            ChatDesigner privates = ChatDesigner.NewDesigner()
               .UseMessageHandler(privateMessages);

            var builder = BotBuilder.NewBuilder("YOUR_TOKEN");
            builder.AddService(builder)

            var bot = builder.EnablePrivates(privates)
               .Build(); 

            await bot.Listen(); 
        }

        private static DefaultCommand StartCommand => new("start", Do_StartAsync);
        private static async Task Do_StartAsync(SignedMessageTextUpdate update)
        {
            await update.Owner.Bot.SendTextMessageAsync(update.ChatId, "Idi naxyi"); 
        }
    }
}
