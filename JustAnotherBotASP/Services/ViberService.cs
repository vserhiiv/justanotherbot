using JustAnotherBotASP.Models;
using JustAnotherBotASP.Models.DbModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DbUser = JustAnotherBotASP.Models.DbModels.User;

namespace JustAnotherBotASP.Services
{
    public class ViberService
    {
        private UserContext _db;
        public ViberService(UserContext db)
        {
            _db = db;
        }

        public async Task ProcessMessageEventAsync(string receiverId, string userMessage, string trackingData)
        {
            switch (userMessage)
            {
                case Constants.setNumberAction:
                    await SendNumberPickerDialogAsync(receiverId);
                    break;
                case Constants.setColorAction:
                    await SendColorPickerDialogAsync(receiverId);
                    break;
                default:
                    await ProcessUserAnswerAsync(receiverId, trackingData, userMessage);
                    break;
            }
            
        }
        private async Task ProcessUserAnswerAsync(string receiverId, string trackingData, string userInput)
        {

            switch (trackingData)
            {
                case Constants.numberPicker:
                    await ProcessPickedNumberAsync(receiverId, userInput);
                    break;
                case Constants.colorPicker:
                    await ProcessPickedColorAsync(receiverId, userInput);
                    break;
                default:
                    await SendTextMessageAsync("What's up?", receiverId);                    
                    break;
            }
            await SendMainMenuAsync(receiverId);
        }

        private async Task ProcessPickedNumberAsync(string receiverId, string userInput)
        {
            var user = await _db.Users.Where(x => x.ViberId == receiverId).FirstOrDefaultAsync();
            
            if(user == null)
            {
                user = new DbUser();
                user.ViberId = receiverId;
                _db.Users.Add(user);

                await _db.SaveChangesAsync();
            }

            user.FavoriteNumber = int.Parse(userInput);

            await _db.SaveChangesAsync();

        }

        private async Task ProcessPickedColorAsync(string receiverId, string userInput)
        {
            var user = await _db.Users.Where(x => x.ViberId == receiverId).FirstOrDefaultAsync();

            if (user == null)
            {
                user = new DbUser();
                user.ViberId = receiverId;
                _db.Users.Add(user);

                await _db.SaveChangesAsync();
            }

            user.FavoriteColorName = userInput;

            await _db.SaveChangesAsync();
        }

        private Task SendNumberPickerDialogAsync(string receiverId)
        {
            return SendTextMessageAsync("Please type your favorite number.", receiverId, Constants.numberPicker);
        }
        
        private Task SendColorPickerDialogAsync(string receiverId)
        {
            return SendTextMessageAsync("Please type your favorite color.", receiverId, Constants.colorPicker);
        }

        public Task ProcessConversationStartedEventAsync(string receiverId)
        {

            string message = "Welcome! Choose your favorite things!";
            return SendTextMessageAsync(message, receiverId);
        }

        public Task SendMainMenuAsync(string receiverId)
        {
            string message = "Choose your favorite things!";

            return SendMainMenuButtonsAsync(receiverId);
        }

        private async Task SendMessageAsync<T>(T model, string receiverId)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://chatapi.viber.com/pa/send_message"),
                Headers =
                {
                    { "X-Viber-Auth-Token", "4d117fc2d2a7df70-4bbfa3839d322ab5-77a05b5c347623c4" },
                    { HttpRequestHeader.Accept.ToString(), "application/json"}
                },
                Content = new StringContent(JsonConvert.SerializeObject(model))
            };

            var response = await httpClient.SendAsync(request);
            string responseAsString = await response.Content.ReadAsStringAsync();


        }

        private Task SendMainMenuButtonsAsync(string receiverId)
        {
            var model = new SendKeyboardRequest();
            model.receiver = receiverId;
            model.min_api_version = 7;
            model.type = "text";
            model.text = "batonchik";
            model.keyboard = new keyboard()
            {
                DefaultHeight = true,
                BgColor = "#6A55FF",
                Buttons = new List<Button>()
            };

            var favoriteNumberButton = new Button()
            { 
                ActionType = "reply",
                ActionBody = Constants.setNumberAction,
                Text = "What your favorite Number?",
                TextSize = "regular"
            };

            var favoriteColorButton = new Button()
            {
                ActionType = "reply",
                ActionBody = Constants.setColorAction,
                Text = "What your favorite Color?",
                TextSize = "regular"
            };

            model.keyboard.Buttons.Add(favoriteNumberButton);
            model.keyboard.Buttons.Add(favoriteColorButton);

            return SendMessageAsync(model, receiverId);
        }

        private Task SendTextMessageAsync(string message, string receiverId, string trackingData=null)
        {
            var model = new SendMessageRequest();
            model.receiver = receiverId;
            model.min_api_version = 1;
            model.sender = new SenderModel()
            {
                name = "Bot",
                avatar = "http://avatar.example.com"
            };
            model.type = "text";
            model.text = message;
            model.tracking_data = trackingData;

            return SendMessageAsync(model, receiverId);
        }
        
    }
}