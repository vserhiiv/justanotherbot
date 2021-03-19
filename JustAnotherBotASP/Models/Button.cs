using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustAnotherBotASP.Models
{
    public class Button
    {
        public int? Columns { get; set; }
        public int? Rows { get; set; }
        public string BgColor { get; set; }
        public string BgMediaType { get; set; }
        public string BgMedia { get; set; }
        public bool? BgLoop { get; set; }
        public string ActionType { get; set; }
        public string ActionBody { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
        public string TextVAlign { get; set; }
        public string TextHAlign { get; set; }
        public int? TextOpacity { get; set; }
        public string TextSize { get; set; }
    }
}