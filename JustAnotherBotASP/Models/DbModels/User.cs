using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustAnotherBotASP.Models.DbModels
{
    public class User
    {
        public int Id { get; set; }
        public string ViberId { get; set; }
        public int FavoriteNumber { get; set; }
        public string FavoriteColorName { get; set; }        
    }
}