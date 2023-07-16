using Microsoft.Extensions.Logging;
using MovieTagApp.Application.Interfaces;
using MovieTagApp.Application.Services;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace MovieTagTelegramBot.Services;

public class UpdateHandler : IUpdateHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<UpdateHandler> _logger;
    private readonly IAddMovieRequestService _addMovieRequestService;

    private static State state;

    public UpdateHandler(ITelegramBotClient botClient, ILogger<UpdateHandler> logger, IAddMovieRequestService _addMovieRequestService)
    {
        _botClient = botClient;
        _logger = logger;
        this._addMovieRequestService = _addMovieRequestService;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient _, Update update, CancellationToken cancellationToken)
    {

        var handler = update switch
        {
            { Message: { } message } => BotOnMessageReceived(message, cancellationToken),            
            _ => UnknownUpdateHandlerAsync(update, cancellationToken)
        };

        await handler;
    }

    private async Task BotOnMessageReceived(Message message, CancellationToken cancellationToken)
    
    {
        _logger.LogInformation("Receive message type: {MessageType}", message.Type);
        if (message.Text is not { } messageText)
            return;

        if (state == State.Wait)
        {
            var action = messageText.Split(' ')[0] switch
            {
    
                "/add" => Add(_botClient, message, cancellationToken),
                _ => Usage(_botClient, message, cancellationToken)
            };

            Message sentMessage = await action;
            return;
        }

        if (state == State.WaitKinopoiskLink)
        {
            state = State.Wait;

            var action = messageText;

            List<string> splitedMessage = messageText.Split("/", StringSplitOptions.RemoveEmptyEntries).ToList();
            int id = int.Parse(splitedMessage[^1]);
            await _addMovieRequestService.CreateAsync(id);
            await _botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Ваша заявка принята",
            cancellationToken: cancellationToken);
            return;
            
        }              


        static async Task<Message> Add(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            state = State.WaitKinopoiskLink;

            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Введите ссылку на фильм на кинопоиске",
                cancellationToken: cancellationToken);
            
        }


        

        static async Task<Message> Usage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            const string usage = "Usage:\n" +                                 
                                 "/add - введите ссылку на Кинопоиск\n" +
                                 "/get - введите теги\n";


            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: usage,
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);
        }
       

        static Task<Message> FailingHandler(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            throw new IndexOutOfRangeException();
        }

    }

    private Task UnknownUpdateHandlerAsync(Update update, CancellationToken cancellationToken)

    {
        _logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
        return Task.CompletedTask;
    }

    public async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        _logger.LogInformation("HandleError: {ErrorMessage}", ErrorMessage);

        // Cooldown in case of network connection error
        if (exception is RequestException)
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
    }
}
