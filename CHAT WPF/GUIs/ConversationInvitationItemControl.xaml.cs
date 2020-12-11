using CHAT_WPF.Models;
using CHAT_WPF.Services;
using CHAT_WPF.Utilities;
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

namespace CHAT_WPF.GUIs
{
    /// <summary>
    /// Interaction logic for ConversationInvitationItemControl.xaml
    /// </summary>
    public partial class ConversationInvitationItemControl : UserControl
    {
        private string Status { get; set; }
        public KeyValuePair<string,UserModel> Model { get; set; }
        public ConversationInvitationWindow ConversationInvitationWindow { get; set; }
        public ConversationInvitationItemControl(ConversationInvitationWindow conversationInvitationWindow, KeyValuePair<string, UserModel> model, string status = null)
        {
            InitializeComponent();
            ConversationInvitationWindow = conversationInvitationWindow;
            Model = model;
            Status = status;
            Load();
        }

        public void Load()
        {
            if(Model.Value != null)
            {
                FullName.Text = Model.Value.Fullname;
                Avatar.ImageSource = Ultilities.ConvertBase64StringToBitmapImage(Model.Value.Avatar);

                if (Status == MemberModel.Statuses.Joined)
                {
                    Contenstatus.Text = "Joined";
                    InviteButton.IsEnabled = false;
                }
                else
                if (Status == MemberModel.Statuses.Invited)
                {
                    Contenstatus.Text = "invited";
                    InviteButton.IsEnabled = false;
                }
            }
        }

        private void _Event_InviteButton_Click(object sender, RoutedEventArgs e)
        {
            var control = new TextBlock() { Text = Model.Value.Fullname };
            control.FontSize = 10;
            control.Margin = new Thickness(10, 5, 10, 5);

            var border = new Border();
            border.Child = control;
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = Brushes.Gray;
            border.CornerRadius = new CornerRadius(10);
            border.Margin = new Thickness(5);

            this.InviteButton.IsEnabled = false;
            this.InviteButton.Content = MemberModel.Statuses.Invited;
            this.ConversationInvitationWindow.InvitedUsersContainer.Children.Add(border);

            NotificationService.SendInvitationJoinConversation(ConversationInvitationWindow.Model.Key, Service.UserID, Model.Key);
            
        }
    }

}
