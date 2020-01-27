using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat_server.Hubs
{
    public class Chat : Hub
    {
        public void SendToAll(string name, string text)
        {
            Clients.All.SendAsync("SendToAll", name, text);
        }
    }
}
