using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustAnotherBotASP.Models
{
    public class keyboard
    {
        public bool DefaultHeight { get; set; }
        public string BgColor { get; set; }
        public List<Button> Buttons { get; set; }
    }
}