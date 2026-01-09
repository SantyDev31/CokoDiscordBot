using DSharpPlus;
using DSharpPlus.EventArgs;

namespace CokoBot.App.Domain.Interfaces
{
    public interface ITrigger
    {
        bool Matches(string message);
        Task Execute(DiscordClient client, MessageCreateEventArgs @event);
    }
}
