using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustAnotherBotASP.Models
{
    public class User
    {
        public string id { get; set; }
        public string name { get; set; }
        public string avatar { get; set; }
        public string country { get; set; }
        public string language { get; set; }
        public int api_version  { get; set; }
    }
}