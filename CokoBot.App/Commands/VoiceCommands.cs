using CokoBot.App.Interfaces;
using CokoBot.PlayMusic;
using DSharpPlus;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Logging;

namespace CokoBot.App.Commands
{
    public class VoiceCommands
    {
        private static readonly Dictionary<ulong, VoiceMusic> MusicPerGuild = new();
        public class PlayCommand : ICommand
        {
            public string command => "play";

            public async Task Execute(DiscordClient client, MessageCreateEventArgs @event)
            {
                _ = Task.Run(async () =>
                {
                    var guildId = @event.Guild.Id;
                    var msg = @event.Message.Content.IndexOf(' ') >= 0
                        ? @event.Message.Content[(@event.Message.Content.IndexOf(' ') + 1)..].Trim()
                        : string.Empty;

                    if (string.IsNullOrEmpty(msg))
                        return;

                    if (!MusicPerGuild.TryGetValue(guildId, out VoiceMusic? value))
                    {
                        var playMusic = new VoiceMusic(
                            client,
                            Startup.ILoggerFactory.CreateLogger($"{nameof(VoiceMusic)} => {@event.Guild.Name}")
                        );
                        value = playMusic;
                        MusicPerGuild[guildId] = value;
                    }

                    await value.ConnectAsync(@event, msg);
                });
            }
        }
        public class StopCommand : ICommand
        {
            public string command => "stop";

            public async Task Execute(DiscordClient client, MessageCreateEventArgs @event)
            {
                var guildId = @event.Guild.Id;

                if (!MusicPerGuild.TryGetValue(guildId, out VoiceMusic? value))
                {
                    var playMusic = new VoiceMusic(
                        client,
                        Startup.ILoggerFactory.CreateLogger($"{nameof(VoiceMusic)} => {@event.Guild.Name}")
                    );
                    value = playMusic;
                    MusicPerGuild[guildId] = value;
                }

                await value.DisconnectAsync(@event);
            }
        }
    }
}
