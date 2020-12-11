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
using System.Windows.Threading;

namespace CHAT_WPF.GUIs
{
    /// <summary>
    /// Interaction logic for ConversationInvitationRequestControl.xaml
    /// </summary>
    public partial class ConversationInvitationRequestControl : UserControl
    {
        public KeyValuePair<string, NotificationModel> Model { get; set;}

        public ConversationInvitationRequestControl(KeyValuePair<string, NotificationModel> model)
        {
            InitializeComponent();
            Model = model;
        }

        public void Load()
        {
            if(Model.Value != null)
            {
                ConversationInvitationModel content  = Newtonsoft.Json.JsonConvert.DeserializeObject<ConversationInvitationModel>(Model.Value.Content.ToString());
                UserModel fromUser = UserService.GetUserById(content.FromUserID);

                this.FromUserAvatar.ImageSource = Ultilities.ConvertBase64StringToBitmapImage(fromUser.Avatar);
                this.ContentText.Text = "You have received an invitation to join the conversation by " + fromUser.Fullname;

            }
        }

        private void _Event_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.InvokeAsync(() => {
                Load();
            }, DispatcherPriority.ApplicationIdle);
        }

        private void _Event_AcceptButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (Model.Value != null)
            {
                ConversationInvitationModel content = Newtonsoft.Json.JsonConvert.DeserializeObject<ConversationInvitationModel>(Model.Value.Content.ToString());

                NotificationService.AcceptInvitationJoinConversation(content.ConversationID, Model.Key, content.FromUserID, content.ToUserID);

            }
            else
            {
                MessageBox.Show("Error");
            }
            
        }

        private void _Event_RefuseButton_Clicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
