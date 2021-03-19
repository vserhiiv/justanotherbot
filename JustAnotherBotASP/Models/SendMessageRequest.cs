using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustAnotherBotASP.Models
{
    public class SendMessageRequest
    {
        public string receiver { get; set; }
        public int min_api_version { get; set; }
        public SenderModel sender { get; set; }
        public string tracking_data { get; set; }
        public string type { get; set; }
        public string text { get; set; }
    }
}