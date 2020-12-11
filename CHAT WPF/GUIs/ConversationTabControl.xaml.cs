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

namespace CHAT_WPF.GUIs
{
    /// <summary>
    /// Interaction logic for ConversationTabControl.xaml
    /// </summary>
    public partial class ConversationTabControl : UserControl
    {
        public MessageTabModel Model { get; set; }
        public ConversationTabControl()
        {
            InitializeComponent();

            Model = new MessageTabModel();

        }

        //public void OpenConversation(string ConversationID)
        //{
        //    if (!string.IsNullOrEmpty(ConversationID))
        //    {
        //        CurrentConversationContainer.Children.Clear();
        //        CurrentConversationContainer.Children.Add(new CHAT_WPF.GUIs.ConversationBoxControl(ConversationID));
        //    }
        //    else
        //    {
        //        MessageBox.Show("Error: CoversationID null");
        //    }
        //}

        public void OpenConversation(KeyValuePair<string, ConversationModel> conversation)
        {
            if (conversation.Value != null && Model.CurrentConversation.Key != conversation.Key)
            {
                Model.CurrentConversation = conversation;
                CurrentConversationContainer.Children.Clear();
                CurrentConversationContainer.Children.Add(new CHAT_WPF.GUIs.ConversationBoxControl(conversation));
            }
            else
            {
                MessageBox.Show("Error: Conversation null");
            }
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
                    this.ListConversationsContainer.Children.Add(new ConversationItemControl(this, conversarion));
                }
            }
        }

    }
}
