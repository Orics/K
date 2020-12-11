using CHAT_WPF.GUIs;
using CHAT_WPF.Models;
using CHAT_WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CHAT_WPF
{
    /// <summary>
    /// Interaction logic for NotificationBox.xaml
    /// </summary>
    public partial class NotificationBox : UserControl
    {
        public NotificationBox()
        {
            InitializeComponent();
        }

        public void Load()
        {
            var notifications = NotificationService.GetNotificationsOfUser(Service.UserID);
            if(notifications != null)
            {
                foreach (var notif in notifications)
                {
                    if(notif.Value.NotificationType == NotificationTypes.ConversationInvitationRequest)
                    {
                        NotificationContainer.Children.Add(new ConversationInvitationRequestControl(notif));
                    }
                    else
                    if (notif.Value.NotificationType == NotificationTypes.ConversationInvitationNotification)
                    {
                       
                    }
                }
            }
        }

        private void _Event_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.InvokeAsync(() => {
                Load();
            }, DispatcherPriority.ApplicationIdle);
        }
    }
}
