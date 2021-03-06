﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace OpenttdDiscord.Backend.Servers
{
    public class SubscribedServerService : ISubscribedServerService
    {
        private readonly IServerRepository serverRepository;

        private readonly ISubscribedServerRepository subscribedServerRepository;

        public SubscribedServerService(IServerRepository serverRepo, ISubscribedServerRepository subServRepo)
        {
            this.serverRepository = serverRepo;
            this.subscribedServerRepository = subServRepo;
        }

        public async Task<SubscribedServer> AddServer(string ip, int port, ulong channelId)
        {
            if (await this.subscribedServerRepository.Exists(ip, port, channelId))
                return await this.subscribedServerRepository.Get(ip, port, channelId);

            Server server = await this.serverRepository.GetServer(ip, port);

            if(server == null)
            {
                server = await this.serverRepository.AddServer(ip, port);
            }

            return await this.subscribedServerRepository.Add(server, channelId);
        }

        public Task<bool> Exists(string ip, int port, ulong channelId) => this.subscribedServerRepository.Exists(ip, port, channelId);

        public Task<IEnumerable<SubscribedServer>> GetAllServers() => this.subscribedServerRepository.GetAll();
        public Task UpdateServer(ulong serverId, ulong channelId, ulong messageId) => this.subscribedServerRepository.UpdateServer(serverId, channelId, messageId);
    }
}
