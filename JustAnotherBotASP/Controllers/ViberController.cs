using JustAnotherBotASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using JustAnotherBotASP.Services;
using System.Threading.Tasks;
using JustAnotherBotASP.Models.DbModels;

namespace JustAnotherBotASP.Controllers
{
    public class ViberController : Controller
    {
        private readonly ViberService _viberService;
        public ViberController()
        {
            _viberService = new ViberService(new UserContext());
        }
        
        [HttpPost]
        public async Task<string> Process( ViberWebhookData model)
        { 

            switch (model.Event)
            {
                case "conversation_started":
                    await _viberService.ProcessConversationStartedEventAsync(model.user.id);
                    await _viberService.SendMainMenuAsync(model.user.id);
                    break;
                case "message":
                    await _viberService.ProcessMessageEventAsync(model.Sender.id, model.Message.text, model.Message.tracking_data);
                break;
                
            }
            
            return "OK";
        }
    }
}