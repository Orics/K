using CHAT_WPF.GUIs;
using CHAT_WPF.Models;
using CHAT_WPF.Services;
using CHAT_WPF.Utilities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace CHAT_WPF
{
    /// <summary>
    /// Interaction logic for MessageContainer.xaml
    /// </summary>
    public partial class MessageContainer : UserControl
    {
        #region properties

        private DateTime LoadedTime;
        private DateTime SeenTime;
        public KeyValuePair<string, ConversationModel> Model { get; set; }

        #endregion


        #region contructor

        public MessageContainer(KeyValuePair<string, ConversationModel> model)
        {
            InitializeComponent();
            Model = model;
            Load();
            OnAsyns();
        }

        #endregion


        #region methods

        private void OnAsyns()
        {
            Service.Client.OnAsync(
                path: "Conversations/" + Model.Key + "/ChangedTime",
                changed: (s, args, context) =>
                {
                    if (DateTime.Parse(args.Data) > this.LoadedTime)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            LoadChange(ConversationService.GetConversationById(Model.Key));
                        });
                    }
                }
            );
        }

        public void LoadChange(KeyValuePair<string, ConversationModel> model)
        {
            // Load new recived messages
            var messages = model.Value.Messages.Where(m => m.Value.SendTime > this.LoadedTime && m.Value.UserID != Service.UserID)
                                               .OrderBy(m => m.Value.SendTime)
                                               .ToDictionary(m => m.Key, m => m.Value);
            if(messages.Count > 0)
            {
                foreach (var message in messages)
                {
                    this.MessageContainer1.Children.Add(
                        new UserControlMessageReceived()
                        {
                            Model = message
                        }
                    );
                }

                // update model
                this.Model = model;
                // update loaded time
                this.LoadedTime = messages.Values.Last<MessageModel>().SendTime;

            } 
        }

        public void Load()
        {
            // Load conversation title

            // Load conversation avatar

            // Load messages
            this.MessageContainer1.Children.Clear();
            foreach (var message in Model.Value.Messages.OrderBy(m => m.Value.SendTime))
            {
                if (message.Value.UserID == UserService.UserID)
                {
                    this.MessageContainer1.Children.Add(new SentMessageControl(message));
                }
                else
                {
                    this.MessageContainer1.Children.Add(
                        new UserControlMessageReceived()
                        {
                            Model = message
                        }
                    );

                }
            }

            // Load system emojis
            if (SystemValues.Emojis != null)
            {
                foreach (var emoji in SystemValues.Emojis)
                {
                   // SystemEmojisContainer.Children.Add(new EmojiControl(this, emoji));
                }
            }

            // Load system stickers
            if (SystemValues.Stickers != null)
            {
                foreach (var sticker in SystemValues.Stickers)
                {
                   // SystemStickerContainer.Children.Add(new StickerControl(this, sticker));
                }
            }  

            this.LoadedTime = DateTime.Now;
        }

        private void ResetMessageInput()
        {
            MessageFilesContainer.Children.Clear();
            MessageImagesContainer.Children.Clear();
            ConversationInput.Text = "";
        }

        #endregion


        #region events

        private void _EventClickSendMessage(object sender, RoutedEventArgs e)
        {
            List<MessageUploadFileModel> Files = new List<MessageUploadFileModel>();
            foreach (UploadFileControl file in MessageFilesContainer.Children)
            {
                Files.Add(file.Model);
            }

            List<MessageUploadFileModel> Images = new List<MessageUploadFileModel>();
            foreach (UploadImageControl file in MessageImagesContainer.Children)
            {
                Images.Add(file.Model);
            }

            UnsentMessageModel message = new UnsentMessageModel()
            {
                UserID = Service.UserID,
                ConversationID = Model.Key,
                Text = ConversationInput.Text,
                Files = Files,
                Images = Images,
            };

            this.MessageContainer1.Children.Add(new SentMessageControl(message));

            // reset input
            ResetMessageInput();
        }

        private void _Event_UploadFileButton_Clicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "All Files (*.*)|*.*";
            dialog.FilterIndex = 1;

            if (dialog.ShowDialog() == true)
            {
                var control = new UploadFileControl(new MessageUploadFileModel()
                {
                    ConversationID = Model.Key,
                    FileName = System.IO.Path.GetFileName(dialog.FileName),
                    FilePath = dialog.FileName,
                });

                MessageFilesContainer.Children.Add(control);
            }
        }

        private void _Event_UploadImageButton_Clicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Files | *.jpg; *.jpeg; *.png;";
            dialog.FilterIndex = 1;

            if (dialog.ShowDialog() == true)
            {
                var control = new UploadImageControl(new MessageUploadFileModel()
                {
                    ConversationID = Model.Key,
                    FileName = System.IO.Path.GetFileName(dialog.FileName),
                    FilePath = dialog.FileName,
                });

                MessageImagesContainer.Children.Add(control);
            }
        }

        private void _Event_ConversationInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                if (ConversationInput.Text.Contains(":"))
                {
                    var code = ConversationInput.Text.Substring(ConversationInput.Text.LastIndexOf(':'));
                    var emoji = Ultilities.ConvertCodeToEmoji(code);
                    if (emoji != null)
                    {
                        ConversationInput.Text = ConversationInput.Text.Replace(code, emoji);
                        ConversationInput.SelectionStart = ConversationInput.Text.Length;
                    }
                }
                
            }
        }

        private void _Event_UploadEmojiButton_Clicked(object sender, RoutedEventArgs e)
        {
            if(EmojiTab.Visibility == Visibility.Visible)
            {
                EmojiTab.Visibility = Visibility.Collapsed;
            }
            else
            {
                EmojiTab.Visibility = Visibility.Visible;
                StickerTab.Visibility = Visibility.Collapsed;
            }
        }

        private void _Event_UploadStickerButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (StickerTab.Visibility == Visibility.Visible)
            {
                StickerTab.Visibility = Visibility.Collapsed;
            }
            else
            {
                StickerTab.Visibility = Visibility.Visible;
                EmojiTab.Visibility = Visibility.Collapsed;
            }
        }

        #endregion

        

    }
}