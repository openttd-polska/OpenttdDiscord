﻿using Discord;
using Discord.Commands;
using OpenttdDiscord.Backend.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenttdDiscord.Commands
{
    public class ServerCommands : ModuleBase<SocketCommandContext>
    {
        public ISubscribedServerService SubscribedServerService { get; set; }

        [Command("ping")]
        [Alias("pong", "hello")]
        public Task PingAsync()
            => ReplyAsync("pong!");

        [Command("register_server")]
        [RequireContext(ContextType.Guild, ErrorMessage = "Sorry, this command must be ran from within a server, not a DM!")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task RegisterServer(string ip, int port)
        {
            if(await this.SubscribedServerService.Exists(ip, port, Context.Channel.Id))
            {
                await ReplyAsync("Server is already registered on this channel");
                return;
            }
            await this.SubscribedServerService.AddServer(ip, port, Context.Channel.Id);
            await ReplyAsync("Server has been registered!");
        }

    }
}
