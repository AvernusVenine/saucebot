using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace Saucebot
{
    public class DiscordBot
    {
        private const int RETRY_INTERVAL = 10000;
        private const int RUN_INTERVAL = 1000;

        private DiscordSocketClient client;
        private CommandService commands;

        private string token;
        private string connectionStatus;

        private bool connected;
        private bool retryConnection;
        private bool running;

        public async Task Start()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();

            while (true)
            {
                try
                {

                    await client.LoginAsync(TokenType.Bot, token);
                    await client.StartAsync();

                    await InstallCommands();

                    running = true;

                    break;
                }
                catch
                {
                    Console.WriteLine("Failed to connect.");

                    running = false;

                    if (!retryConnection)
                    {
                        connectionStatus = "Disconnected";
                        return;
                    }

                    await Task.Delay(RETRY_INTERVAL);
                }
            }

            while (running) await Task.Delay(RUN_INTERVAL);

            if(client.ConnectionState == ConnectionState.Connecting || client.ConnectionState == ConnectionState.Connected)
            {
                try
                {
                    client.StopAsync().Wait();
                }
                catch { }
            }

        }

        public async Task InstallCommands()
        {
            if (client.LoginState != LoginState.LoggedIn) return;

            commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async
            });

            //Add commands here

            client.MessageReceived += MessageRecieved;
            client.ReactionAdded += ReactionAdded;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), null); //May need to make an IServiceProvider unsure tho
        }

        public async Task MessageRecieved(SocketMessage SOCKET_MESSAGE)
        {
            var message = SOCKET_MESSAGE as SocketUserMessage;
            var context = new SocketCommandContext(client, message);

            if (context.User.IsBot) return;
            if (!message.Content.StartsWith("??")) return;


        }

        public async Task ReactionAdded(Cacheable<IUserMessage, ulong> MESSAGE, ISocketMessageChannel CHANNEL, SocketReaction SOCKET_REACTION)
        {
            var reaction = SOCKET_REACTION.Emote;
            //reaction.
        }

        public void SetBotToken(string FILE_PATH)
        {
            using (StreamReader file = new StreamReader(FILE_PATH))
            {
                token = file.ReadLine();
            }
        }
    }
}
