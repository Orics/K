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
    /// Interaction logic for SentMessageControl.xaml
    /// </summary>
    public partial class SentMessageControl : UserControl
    {
        public KeyValuePair<string, MessageModel> Model { get; set; }

        public SentMessageControl(KeyValuePair<string, MessageModel> model)
        {
            InitializeComponent();
            this.HorizontalAlignment = HorizontalAlignment.Right;
            Model = model;
            Load();
        }
        public SentMessageControl(UnsentMessageModel model)
        {
            InitializeComponent();
            this.HorizontalAlignment = HorizontalAlignment.Right;  
            Load(model);
            SendMessageAsync(model);
        }

        private void Load()
        {
            if (Model.Value != null)
            {
                if (!string.IsNullOrEmpty(Model.Value.Sticker))
                {
                    MessageEmojiContainer.Children.Add(new MessageStickerControl(Model.Value.Sticker));
                }
                else
                {
                    if (Model.Value.Images != null)
                    {
                        foreach (MessageFileModel file in Model.Value.Images)
                        {
                            this.MessageImagesContainer.Children.Add(new MessageImageControl(file));
                        }
                    }

                    if (Model.Value.Files != null)
                    {
                        foreach (MessageFileModel file in Model.Value.Files)
                        {
                            this.MessageFilesContainer.Children.Add(new MessageFileControl(file));
                        }
                    }

                    if (!string.IsNullOrEmpty(Model.Value.Text))
                    {
                        MessageTextContainer.Child = new TextBox()
                        {
                            Text = Model.Value.Text
                        };
                    }
                }

                SendTime.Visibility = Visibility.Visible;
                SendTime.Text = Model.Value.SendTime.ToString();
            }

            SentLoading.Visibility = Visibility.Collapsed;
        }

        private void Load(UnsentMessageModel model)
        {
            if (model != null)
            {
                Model = new KeyValuePair<string, MessageModel>(null, model.ConvertToMessageModel());

                if (!string.IsNullOrEmpty(model.Sticker))
                {
                    MessageEmojiContainer.Children.Add(new MessageStickerControl(model.Sticker));
                }
                else
                {
                    if (model.Images != null)
                    {
                        foreach (MessageUploadFileModel file in model.Images)
                        {
                            this.MessageImagesContainer.Children.Add(new MessageImageControl(file));
                        }
                    }

                    if (model.Files != null)
                    {
                        foreach (MessageUploadFileModel file in model.Files)
                        {
                            this.MessageFilesContainer.Children.Add(new MessageFileControl(file));
                        }
                    }

                    if (!string.IsNullOrEmpty(model.Text))
                    {
                        MessageTextContainer.Child = new TextBox()
                        {
                            Text = Model.Value.Text
                        };
                    }
                }    
            }
        }

        private async Task SendMessageAsync(UnsentMessageModel model) 
        {
            var messageId = await ConversationService.SendMessageToConversationAsync(model); 
            
            if (!string.IsNullOrEmpty(messageId))
            {
                SentLoading.Visibility = Visibility.Collapsed;
                SendTime.Visibility = Visibility.Visible;
                SendTime.Text = "28/11/2020";
                // update message id
                Model = new KeyValuePair<string, MessageModel>(messageId, Model.Value);
            }
            else
            {
                SentLoading.Visibility = Visibility.Collapsed;
                SendTime.Visibility = Visibility.Visible;
                SendTime.Text = "Error";
                // update message id
                Model = new KeyValuePair<string, MessageModel>(null, Model.Value);
            }
        }
    }
}
