using System;
using Discord;
using Discord.WebSocket;

namespace testbot
{
    class Program
    {
        DiscordSocketClient client;
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();
        private async Task MainAsync()
        {
            var config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildMessages | GatewayIntents.MessageContent
            };

            client = new DiscordSocketClient(config);
            client.MessageReceived += CommandHandler;
            client.Log += Log;

            var token = "MTI0MzI1ODY0ODEyMjEwMTkzMQ.G-CcWC.nuleAUGKXdLtE2d3mrxm7nTd_L9-6PcltCx1xc";

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            Console.ReadLine();
        }

        private Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }

        private async Task CommandHandler(SocketMessage message)
        {
            if (!message.Author.IsBot)
                switch (message.Content)
                {
                    case "!радость":
                        var emoji = new Emoji("😄");
                        await AddEmotion(message, emoji);
                        break;
                    case "!грусть":
                        emoji = new Emoji("😩");
                        await AddEmotion(message, emoji);
                        break;
                    case "!клоун":
                        emoji = new Emoji("🤡");
                        await AddEmotion(message, emoji);
                        break;

                }
        }
        private async Task AddEmotion(SocketMessage message, Emoji emoji)
        {
            Emoji currentEmoji = emoji;
            await message.AddReactionAsync(currentEmoji);
        }
    }
}