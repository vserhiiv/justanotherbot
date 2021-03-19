using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustAnotherBotASP.Models
{
    public class ViberWebhookData
    {
        public string Event { get; set; }

        public long TimeStamp { get; set; }

        public string Chat_HostName { get; set; }

        public string Message_Token { get; set; }

        public bool Silent { get; set; }

        public SenderModel Sender { get; set; }

        public MessageModel Message { get; set; }

        public User user { get; set; }
    }
}