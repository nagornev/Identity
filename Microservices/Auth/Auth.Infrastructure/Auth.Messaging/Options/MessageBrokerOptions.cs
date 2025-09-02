using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Messaging.Options
{
    public class MessageBrokerOptions
    {
        public MessageBrokerOptions(string host,
                                    ushort port,
                                    string username,
                                    string password)
        {
            Host = host;
            Port = port;
            Username = username;
            Password = password;
        }

        public string Host { get; }

        public ushort Port { get; }

        public string Username { get; }

        public string Password { get; }
    }
}
