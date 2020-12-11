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
using WpfAnimatedGif;

namespace CHAT_WPF.GUIs
{
    /// <summary>
    /// Interaction logic for StickerControl.xaml
    /// </summary>
    public partial class StickerControl : UserControl
    {
        public string Source { get; set; }
        public ConversationBoxControl ConversationControl { get; set; }
        public StickerControl(ConversationBoxControl conversationControl, string source)
        {
            InitializeComponent();
            ConversationControl = conversationControl;
            Source = source;
            ImageBehavior.SetAnimatedSource(StickerContent, Ultilities.ConvertBase64StringToBitmapImage(source));

        }

        private void _Event_Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(ConversationControl != null)
            {
                ConversationControl.MessageContainer1.Children.Add(new SentMessageControl(new UnsentMessageModel()
                {
                    UserID = Service.UserID,
                    ConversationID = ConversationControl.Model.Key,
                    Sticker = Source,
                    Text = null,
                    Files = null,
                    Images = null
                }));

                ConversationControl.StickerTab.Visibility = Visibility.Collapsed;
            }
           
        }

        private void _Event_StickerBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            StickerBorder.BorderBrush = Brushes.Blue;
        }

        private void _Event_StickerBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            StickerBorder.BorderBrush = Brushes.Transparent;
        }
    }
}
