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
    /// Interaction logic for ConversationItemControl.xaml
    /// </summary>
    public partial class ConversationItemControl : UserControl
    {
        public bool IsShowing { get; set; }
        public KeyValuePair<string, ConversationModel> Model { get; set; }

        public ConversationTabControl ConversationTab { get; set; }

        public ConversationItemControl(ConversationTabControl conversationTab, KeyValuePair<string, ConversationModel> model)
        {
            InitializeComponent();
            ConversationTab = conversationTab;
            Model = model;
            Load();
            OnAsyns();
        }

        private void OnAsyns()
        {
            //new Thread(new ThreadStart(() =>
            //{

            //})).Start();

            Service.Client.OnAsync(
                path: "Conversations/" + Model.Key,
                changed: (sender, args, context) =>
                {
                    if (args.Path == "/Title")
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            this.ConversationTitle.Text = args.Data;
                        });
                    }
                    else
                    if (args.Path == "/Avatar")
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            this.ConversationAvatar.ImageSource = Ultilities.ConvertBase64StringToBitmapImage(args.Data);
                        });
                    }
                    else
                    if (args.Path == "/ChangedTime" && IsShowing == false)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            Model = ConversationService.GetConversationById(Model.Key);
                            Load();
                        });
                    }
                    else
                    if (args.Path == "/Members/" + Service.UserID + "/SeenTime")
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            Model = ConversationService.GetConversationById(Model.Key);
                            Load();
                        });
                    }
                }
            );
        }

        public void Load()
        {
            if (Model.Value != null)
            {
                // avatar
                this.ConversationAvatar.ImageSource = Ultilities.ConvertBase64StringToBitmapImage(Model.Value.Avatar);

                // unseen messages
                var user = Model.Value.Members.Where(m => m.Key == Service.UserID).FirstOrDefault();
                var unSeenMessages = Model.Value.Messages.Where(m => m.Value.SendTime > user.Value.SeenTime).ToDictionary(m => m.Key, m => m.Value);

                if (unSeenMessages != null && unSeenMessages.Count > 0)
                {
                    // unsent message count
                    this.ConversationUnReadMessageCountBorder.Visibility = Visibility.Visible;
                    this.ConversationUnReadMessageCount.Text = unSeenMessages.Count().ToString();

                    // last message content
                    var lastMessage = unSeenMessages.Last();
                    if (!string.IsNullOrEmpty(lastMessage.Value.Text))
                    {
                        this.ConversationLastMessage.Text = lastMessage.Value.Text;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(lastMessage.Value.Sticker))
                        {
                            this.ConversationLastMessage.Text = "Sticker...";
                        }
                    }

                    this.ConversationTitle.FontWeight = FontWeights.Bold;
                }
                else
                {
                    this.ConversationUnReadMessageCountBorder.Visibility = Visibility.Hidden;
                    this.ConversationTitle.FontWeight = FontWeights.Regular;
                    this.ConversationLastMessage.Text = Model.Value.Messages.Last().Value.Text == null ? "" : Model.Value.Messages.Last().Value.Text;
                }

                // title
                this.ConversationTitle.Text = Model.Value.Title;

            }
        }

        public void ShowConversation()
        {
            if (ConversationTab != null && Model.Value != null)
            {
                MessageBox.Show("Open conversation");
                ConversationTab.OpenConversation(Model);
                IsShowing = true;
            }

        }

        private void _Event_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ConversationTab.Model.CurrentConversation.Key != Model.Key)
            {
                ShowConversation();
            }
        }
    }
}
