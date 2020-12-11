using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHAT_WPF.Models
{
    public struct NotificationTypes
    {
        public static string SystemNotification = "SystemNotification";
        public static string ConversationInvitationNotification = "ConversationInvitationNotification";
        public static string FriendshipInvitationNotification = "FriendshipInvitationNotification";
        public static string ConversationInvitationRequest = "ConversationInvitationRequest";
        public static string FriendshipInvitationRequest = "FriendshipInvitationRequest";
    }

    public struct NotificationStatuses
    {
        public static string Accept = "Accept";
        public static string Refuse = "Refuse";
    }

    public class NotificationModel
    {    
        public string NotificationType { get; set; }
        public object Content { get; set; }
        public DateTime SeenTime { get; set; }
        public DateTime ChangeTime { get; set; }
    }
}
