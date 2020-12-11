using CHAT_WPF.Models;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CHAT_WPF.Services
{
    public class ConversationService : Service
    {
        public static Dictionary<string, ConversationModel> GetConversations()
        {
            if (!string.IsNullOrEmpty(UserID))
            {
                var conversations = Client.Get("Conversations").ResultAs<Dictionary<string, ConversationModel>>()
                                          .Where(c => c.Value.Members.Any(m => m.Key == UserID && m.Value.Status == MemberModel.Statuses.Joined))
                                          .ToDictionary(c => c.Key, c => c.Value);

                return conversations;
            }
            return null;
        }

        public static KeyValuePair<string, ConversationModel> GetConversationById(string ConversationID)
        {

            var conv = Client.Get("Conversations").ResultAs<Dictionary<string, ConversationModel>>()
                             .Where(c => c.Key == ConversationID).FirstOrDefault();

            return conv;
        }

        public static KeyValuePair<string, MessageModel> GetMessageOfConversationById(string conversationID, string messageId)
        {
            var message = Client.Get("Conversations/" + conversationID + "/Messages/" + messageId)
                                          .ResultAs<Dictionary<string, MessageModel>>()
                                          .FirstOrDefault();

            return message;
        }

        public static Dictionary<string, MessageModel> GetMessagesOfConversation(string conversationID)
        {
            var messages = Client.Get("Conversations/" + conversationID + "/Messages")
                                 .ResultAs<Dictionary<string, MessageModel>>()
                                 .ToDictionary(m => m.Key, m => m.Value);
            return messages;
        }

        public static Dictionary<string, UserModel> GetUsersOfConversation(string conversationID)
        {
            var members = Client.Get("Conversations/" + conversationID + "/Members")
                                 .ResultAs<Dictionary<string, MemberModel>>()
                                 .ToDictionary(m => m.Key, m => m.Value);

            var users = new Dictionary<string, UserModel>();
            foreach (var member in members)
            {
                users.Add(member.Key, UserService.GetUserById(member.Key));
            }

            return users;
        }

        public static Dictionary<string, MemberModel> GetUsersEnteringOfConversation(string conversationID)
        {
            var users = Client.Get("Conversations/" + conversationID + "/Members").ResultAs<Dictionary<string, MemberModel>>()
                              .Where(m => m.Value.IsEntering == true)
                              .ToDictionary(c => c.Key, c => c.Value);
            return users;
        }

        public static bool ChangeTitleOfConversation(string conversationID, string Title)
        {
            if (!string.IsNullOrEmpty(conversationID))
            {
                var rsp = Client.Set("Conversations/" + conversationID + "/Title", Title);
                Changed(conversationID);
            }
            return true;
        }

        public static bool ChangeAvatarOfConversation(string conversationID, string ImgPath)
        {
            var stream = File.Open(ImgPath, FileMode.Open);
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            var imgbase64 = Convert.ToBase64String(ms.GetBuffer());
            ConversationService.Client.Set("Conversations/" + conversationID + "/Avatar/", imgbase64);
            Changed(conversationID);
            return true;
        }

        public static bool InviteToJoinConversation(string conversationID, string userID)
        {
            if (!string.IsNullOrEmpty(conversationID) && !string.IsNullOrEmpty(userID))
            {
                // Thêm user vào cuộc hôi thoại
                Client.Push(
                    path: "Conversations/" + conversationID + "/Members/" + userID,
                    data: new MemberModel()
                    {
                        IsEntering = false,
                        Status = MemberModel.Statuses.Invited,
                    }
                );

                // Gửi thông báo cho user

            }
            return true;
        }

        public static string SendMessageToConversation(string conversationID, MessageModel message)
        {
            var rsp = Client.Push("Conversations/" + conversationID + "/Messages", message);

            if (rsp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Changed(conversationID);
                return rsp.Result.name;
            }
            else
            {
                return null;
            }
        }

        public static async Task<string> SendMessageToConversationAsync(UnsentMessageModel model)
        {
            if (model != null)
            {
                var rsp = await Client.PushAsync("Conversations/" + model.ConversationID + "/Messages", model.ConvertToMessageModel());

                if (rsp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Changed(model.ConversationID);
                    return rsp.Result.name;
                }
            }

            return null;
        }

        public static void Changed(string conversationID)
        {
            var rsp = Client.Set("Conversations/" + conversationID + "/ChangedTime", DateTime.Now);
        }

        public static List<string> GetAllSystemStickers()
        {
            var stickers = Client.Get("Stickers").ResultAs<List<string>>();

            return stickers;
        }

        public static Dictionary<string, string> GetAllSystemEmojis()
        {
            var emojis = Client.Get("Emojis").ResultAs<Dictionary<string, string>>().ToDictionary(e => e.Key, e => e.Value); ;

            return emojis;
        }

        public static void ChangeMemberIsEntering(string conversationID, string userID, bool isEntering)
        {
            if (!string.IsNullOrEmpty(conversationID) && !string.IsNullOrEmpty(userID))
            {
                Client.Set("Conversations/" + conversationID + "/Members/" + userID + "/IsEntering", isEntering);
            }
        }

        public static void ChangeMemberSeenTime(string conversationID, string userID, DateTime seenTime)
        {
            if (!string.IsNullOrEmpty(conversationID) && !string.IsNullOrEmpty(userID))
            {
                Client.Set("Conversations/" + conversationID + "/Members/" + userID + "/SeenTime", seenTime);
            }
        }
    }
}