using Microsoft.Extensions.Logging;
using MovieTagTelegramBot.Abstract;
using Telegram.Bot;

namespace MovieTagTelegramBot.Services;

// Compose Receiver and UpdateHandler implementation
public class ReceiverService : ReceiverServiceBase<UpdateHandler>
{
    public ReceiverService(
        ITelegramBotClient botClient,
        UpdateHandler updateHandler,
        ILogger<ReceiverServiceBase<UpdateHandler>> logger)
        : base(botClient, updateHandler, logger)
    {
    }
}
