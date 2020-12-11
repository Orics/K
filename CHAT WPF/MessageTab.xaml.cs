using CHAT_WPF.Models;
using CHAT_WPF.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CHAT_WPF
{
    /// <summary>
    /// Interaction logic for MessageTab.xaml
    /// </summary>
    public partial class MessageTab : UserControl
    {
        public MessageTabModel Model { get; set; }
        public MessageTab()
        {
            InitializeComponent();

            Model = new MessageTabModel();

        }

        public void OpenConversation(string ConversationID)
        {
            if (!string.IsNullOrEmpty(ConversationID))
            {
               // Model.CurrentConversation = ConversationService.GetConversationById(ConversationID);

                CurrentConversationContainer.Children.Clear();

                //CurrentConversationContainer.Children.Add(
                //   // new CHAT_WPF.GUIs.ConversationBoxControl(ConversationID)
                //);
            }
            else
            {
                MessageBox.Show("Error: CoversationID null");
            }

            //if (Model != null)
            //{
            //    if (!string.IsNullOrEmpty(Model.CurrentConversation.Key) && Model.CurrentConversation.Value != null)
            //    {
            //        var otheruser = Model.CurrentConversation.Value.Members.Where(m => m.Key != UserService.UserID).FirstOrDefault();
            //        var avatar = Model.CurrentConversation.Value.Avatar;
            //        var title = Model.CurrentConversation.Value.Title;
            //        var messages = Model.CurrentConversation.Value.Messages.OrderBy(m => m.Value.SendTime).ToDictionary(m => m.Key, m => m.Value).Values;
            //        var members = Model.CurrentConversation.Value.Members;

            //        //load avatar
            //        if (!string.IsNullOrEmpty(Model.CurrentConversation.Value.Avatar))  // load avatar của cuộc hoi thoại nếu có
            //        {
            //            BitmapImage bi = new BitmapImage();
            //            bi.BeginInit();
            //            bi.StreamSource = new MemoryStream(Convert.FromBase64String(avatar));
            //            bi.EndInit();
            //            //this.ConversationAvatar.ImageSource = bi;
            //        }
            //        else
            //        if (members.Count == 2) // load avatar của đối phương nếu là cuộc đối thoai 2 người
            //        {
            //            BitmapImage bi = new BitmapImage();
            //            bi.BeginInit();
            //            bi.StreamSource = new MemoryStream(Convert.FromBase64String(otheruser.Value.Avatar));
            //            bi.EndInit();
            //            //this.ConversationAvatar.ImageSource = bi;
            //        }

            //        //load title
            //        if (!string.IsNullOrEmpty(Model.CurrentConversation.Value.Title))  // load tên của cuộc hoi thoại nếu có
            //        {
            //           // this.ConversationTitle.Text = Model.CurrentConversation.Value.Title;
            //        }
            //        else
            //        if (members.Count == 2) // load tên của đối phương nếu là cuộc đối thoai 2 người
            //        {
            //            //this.ConversationTitle.Text = otheruser.Value.Fullname;
            //        }

            //        // load messages
            //        Model.CurrentConversation = ConversationService.GetConversationById(Model.CurrentConversation.Key);
            //        if (Model.CurrentConversation.Value.Messages.Values.Count > 0)
            //        {
            //            this.ConversationMessageBox.Children.Clear();
            //            foreach (var message in messages)
            //            {
            //                if (message.UserID == UserService.UserID)
            //                {
            //                    this.MessageContainer.Children.Add(new UserControlMessageSent()
            //                    {
            //                        Model = message
            //                    });
            //                }
            //                else
            //                {
            //                    this.ConversationMessageBox.Children.Add(new UserControlMessageReceived()
            //                    {
            //                        Model = message
            //                    });
            //                }
            //            }
            //        }

            //    }
            //    else
            //    {
            //        MessageBox.Show("Conversation is NULL");
            //    }
            //}
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            Model.Conversations = ConversationService.GetConversations();

            //load list conversations
            this.ListConversationsContainer.Children.Clear();
            if (Model.Conversations != null)
            {
                foreach (var conversarion in Model.Conversations)
                {
                    this.ListConversationsContainer.Children.Add(
                        new ConversationControl()
                        {
                            Model = conversarion,
                           // ConversationTab = this
                        }
                    );
                }
            } 
        }

    }
}