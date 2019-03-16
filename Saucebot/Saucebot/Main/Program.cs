using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace Saucebot
{
    class Program
    {
        private const string TOKEN_PATH = "C:\\Users\\Avernus\\Desktop\\Saucebot\\Saucebot\\Saucebot\\Main\\secret.txt";

        public static void Main(string[] args)
        {
            DiscordBot bot = new DiscordBot();

            bot.SetBotToken(TOKEN_PATH);
            bot.Start().GetAwaiter().GetResult();
        }
    }
}

//https://discordapp.com/oauth2/authorize?client_id=555982332444540931&scope=bot&permissions=116736