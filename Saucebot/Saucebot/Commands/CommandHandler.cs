using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Net;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Saucebot.Commands;
using Image.bing.search.API;

namespace Saucebot.Commands
{
    public class CommandHandler
    {
        private SocketCommandContext context;

        public CommandHandler(SocketCommandContext CONTEXT)
        {
            context = CONTEXT;
        }

        public void SendMessage()
        {
            context.Channel.SendMessageAsync("Testing this");
        }

        public async Task FindSource()
        {
            IReadOnlyCollection<SocketMessage> cachedMessages = context.Channel.CachedMessages;

            foreach (SocketMessage message in cachedMessages)
            {
                foreach(IAttachment attachment in message.Attachments)
                {

                    await FindSource(attachment.Url);

                    return;
                }
            }
        }

        public async Task FindSource(string URL)
        {
            temp.
        }

    }
}
//tineye.com and SHA-1 hash