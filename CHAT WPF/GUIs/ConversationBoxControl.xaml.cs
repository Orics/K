using CHAT_WPF.Models;
using CHAT_WPF.Services;
using CHAT_WPF.Utilities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ConversationBoxControl.xaml
    /// </summary>
    public partial class ConversationBoxControl : UserControl
    {
        #region properties

        private DateTime LoadedTime;
        // private string ConversationID;
        private Dictionary<string, UserModel> UserInfors { get; set; }
        public KeyValuePair<string, ConversationModel> Model { get; set; }

        #endregion


        #region contructor

        public ConversationBoxControl(KeyValuePair<string, ConversationModel> model)
        {
            InitializeComponent();
            Model = model;
        }

        #endregion


        #region methods

        private void OnAsyns()
        {
            //new Thread(new ThreadStart(() =>
            //{

            //})).Start();

            // get event add new message, change title, change avatar 
            Service.Client.OnAsync(
                path: "Conversations/" + Model.Key + "/ChangedTime",
                changed: (sender, args, context) =>
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


            // get event user is entering
            Service.Client.OnAsync(
                path: "Conversations/" + Model.Key + "/Members",
                changed: (sender, args, context) =>
                {
                    var users = ConversationService.GetUsersEnteringOfConversation(Model.Key);
                    UserEnteringContainer.Dispatcher.Invoke(() =>
                    {
                        if (users.Count > 0)
                        {
                            UserEnteringContainer.Children.Clear();
                            foreach (var user in users)
                            {
                                UserModel model = null;
                                UserInfors.TryGetValue(user.Key, out model);
                                UserEnteringContainer.Children.Add(new ConversationUserEnteringControl(model));
                            }

                            UserEnteringWrapper.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            UserEnteringWrapper.Visibility = Visibility.Collapsed;
                        }
                    });
                }
            );
        }

        public void LoadChange(KeyValuePair<string, ConversationModel> model)
        {
            // update model
            this.Model = model;

            // Load conversation avatar
            this.ConversationAvatar.ImageSource = Ultilities.ConvertBase64StringToBitmapImage(Model.Value.Avatar);

            // Load conversation title
            this.ConversationTitle.Text = model.Value.Title;

            // Load new recived messages
            var messages = model.Value.Messages.Where(m => m.Value.SendTime > this.LoadedTime && m.Value.UserID != Service.UserID)
                                               .OrderBy(m => m.Value.SendTime)
                                               .ToDictionary(m => m.Key, m => m.Value);
            if (messages.Count > 0)
            {
                foreach (var message in messages)
                {
                    this.MessageContainer1.Children.Add(new ReceivedMessageControl(message));
                }
            }

            // change seen time
            this.LoadedTime = model.Value.ChangedTime;
            ConversationService.ChangeMemberSeenTime(Model.Key, Service.UserID, this.LoadedTime);

            this.ConversationScrollContainer.ScrollToEnd();

            //this.Loading.Visibility = Visibility.Collapsed;
        }

        public void Load()
        {
            if (Model.Value != null)
            {
                // Load danh sách tạm user để tăng hiệu suất load thông tin user đang nhập văn bản
                this.UserInfors = ConversationService.GetUsersOfConversation(Model.Key);

                // Load conversation avatar
                this.ConversationAvatar.ImageSource = Ultilities.ConvertBase64StringToBitmapImage(Model.Value.Avatar);

                // Load conversation title
                this.ConversationTitle.Text = Model.Value.Title;

                // Load messages
                this.MessageContainer1.Children.Clear();
                foreach (var message in Model.Value.Messages.OrderBy(m => m.Value.SendTime))
                {
                    if (message.Value.UserID == "SYS")
                    {
                        this.MessageContainer1.Children.Add(new SystemMesageControl(message));
                    }
                    else
                    if (message.Value.UserID == UserService.UserID)
                    {
                        this.MessageContainer1.Children.Add(new SentMessageControl(message));
                    }
                    else
                    {
                        this.MessageContainer1.Children.Add(new ReceivedMessageControl(message));
                    }
                }


                // Load system emojis
                if (SystemValues.Emojis != null)
                {
                    foreach (var emoji in SystemValues.Emojis)
                    {
                        SystemEmojisContainer.Children.Add(new EmojiControl(this, emoji));
                    }
                }

                // Load system stickers
                if (SystemValues.Stickers != null)
                {
                    foreach (var sticker in SystemValues.Stickers)
                    {
                        SystemStickerContainer.Children.Add(new StickerControl(this, sticker));
                    }
                }

                // change seen time
                this.LoadedTime = Model.Value.ChangedTime;
                ConversationService.ChangeMemberSeenTime(Model.Key, Service.UserID, this.LoadedTime);

                OnAsyns();

                this.ConversationScrollContainer.ScrollToEnd();

                //this.Loading.Visibility = Visibility.Collapsed;
            }
        }

        private void ResetMessageInput()
        {
            MessageFilesContainer.Children.Clear();
            MessageImagesContainer.Children.Clear();
            ConversationInput.Text = "";
        }

        private void ChangeConversationTitle()
        {
            if (!string.IsNullOrEmpty(ConversationTitle.Text))
            {
                ConversationService.ChangeTitleOfConversation(Model.Key, ConversationTitle.Text);
                this.ConversationTitle.IsReadOnly = true;
            }

        }

        private void ChangeConversationAvatar()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Files | *.jpg; *.jpeg; *.png;";
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == true)
            {
                ConversationService.ChangeAvatarOfConversation(Model.Key, dialog.FileName);
            }

        }

        #endregion


        #region events

        private void _EventClickSendMessage(object sender, RoutedEventArgs e)
        {
            List<MessageUploadFileModel> files = new List<MessageUploadFileModel>();
            foreach (UploadFileControl file in MessageFilesContainer.Children)
            {
                files.Add(file.Model);
            }

            List<MessageUploadFileModel> images = new List<MessageUploadFileModel>();
            foreach (UploadImageControl file in MessageImagesContainer.Children)
            {
                images.Add(file.Model);
            }

            UnsentMessageModel message = new UnsentMessageModel()
            {
                UserID = Service.UserID,
                ConversationID = Model.Key,
                Text = ConversationInput.Text,
                Files = files,
                Images = images,
            };

            // send 
            if (files.Count > 0 || images.Count > 0 || !string.IsNullOrEmpty(ConversationInput.Text))
            {
                this.MessageContainer1.Children.Add(new SentMessageControl(message));
            }

            this.ConversationScrollContainer.ScrollToEnd();

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
            if (EmojiTab.Visibility == Visibility.Visible)
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

        private void _Event_ChangeConversationTitleButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeConversationTitle();
        }

        private void _Event_ChangeConversationAvatarButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeConversationAvatar();
        }

        private void _Event_ConversationTitle_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ConversationTitle.IsReadOnly == true)
            {
                ConversationTitle.IsReadOnly = false;
            }
        }

        private void _Event_ConversationInput_GotFocus(object sender, RoutedEventArgs e)
        {
            ConversationService.ChangeMemberIsEntering(Model.Key, Service.UserID, true);
        }

        private void _Event_ConversationInput_LostFocus(object sender, RoutedEventArgs e)
        {
            ConversationService.ChangeMemberIsEntering(Model.Key, Service.UserID, false);
        }

        private void _Event_AddMemberButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new ConversationInvitationWindow(Model);
            window.Show();
        }

        private void _Event_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.InvokeAsync(() => {
                Load();
            }, DispatcherPriority.ApplicationIdle);
        }

        #endregion


    }
}
