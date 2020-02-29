﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenttdDiscord.Configuration
{
    public class DiscordConfig
    {
        public string Token { get; }

        public DiscordConfig(IConfigurationRoot config)
        {
            const string prefix = "discord:";

            this.Token = config[$"{prefix}token"];

        }
        
    }
}
