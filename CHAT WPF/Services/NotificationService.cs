using CHAT_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHAT_WPF.Services
{
    public class NotificationService : Service
    {
        public static KeyValuePair<string, NotificationModel> GetNotificationOfUserById(string userID, string notificationID)
        {
            var notification = Client.Get("Users/" + userID + "/Notifications")
                                      .ResultAs<Dictionary<string, NotificationModel>>()
                                      .Where(n => n.Key == notificationID).FirstOrDefault();
            return notification;
        }

        public static Dictionary<string, NotificationModel> GetNotificationsOfUser(string userID)
        {
            var notifications = Client.Get("Users/" + userID + "/Notifications")
                                      .ResultAs<Dictionary<string, NotificationModel>>()
                                      .ToDictionary(n => n.Key, n => n.Value);
            return notifications;
        }

        public static bool SendInvitationJoinConversation(string conversationID, string fromUserID, string toUserID)
        {
            //Thêm tạm user vào cuộc hội thoại
            Client.SetAsync("Conversations/" + conversationID + "/Members/" + toUserID, new MemberModel()
            {
                IsEntering = false,
                Status = MemberModel.Statuses.Invited,
            });

            // Thêm thông báo cho người yêu cầu
            Client.PushAsync("Users/" + fromUserID + "/Notifications", new SystemNotificationModel()
            {
                Text = "You have sent " + UserService.GetUserById(toUserID).Fullname +" an invitation to join the conversation."
            }); 

            // Thêm thông báo cho người nhận 
            Client.PushAsync("Users/" + toUserID + "/Notifications", new NotificationModel()
            {
                ChangeTime = DateTime.Now,
                NotificationType = NotificationTypes.ConversationInvitationRequest,
                Content = new ConversationInvitationModel()
                {
                    ConversationID = conversationID,
                    FromUserID = fromUserID,
                    ToUserID = toUserID
                }
            });

            return true;
        }

        public static bool AcceptInvitationJoinConversation(string conversationID, string notificationID, string fromUserID, string toUserID)
        {
            // Xác thực user vào cuộc hội thoại
            Client.SetAsync("Conversations/" + conversationID  + "/Members/" + toUserID, new MemberModel()
            {
                IsEntering = false,
                Status = MemberModel.Statuses.Joined,
            });

            // Thêm thông báo đã tham gia cuộc hội thoại
            Client.PushAsync("Users/" + toUserID + "/Notifications", new SystemNotificationModel()
            {
                Text = "You joined the conversation with " + UserService.GetUserById(fromUserID).Fullname + "."
            });

            // Xóa lời mời cho toUser
            Client.DeleteAsync("Users/" + toUserID + "/Notifications/" + notificationID);

            return true;
        }
    }
}
