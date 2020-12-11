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
    /// Interaction logic for EmojiControl.xaml
    /// </summary>
    public partial class EmojiControl : UserControl
    {
        public KeyValuePair<string, string> Emoji{ get; set; }
        public ConversationBoxControl ConversationControl { get; set; }
        public EmojiControl(CHAT_WPF.GUIs.ConversationBoxControl conversationControl, KeyValuePair<string, string> emoji)
        {
            InitializeComponent();
            Emoji = emoji;
            ConversationControl = conversationControl;
            EmojiContent.Text = Emoji.Value;
            EmojiBorder.ToolTip = new ToolTip() { Content = emoji.Key };
        }

        private void _Event_Content_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(ConversationControl != null)
            {
                ConversationControl.ConversationInput.Text += Emoji.Value;
            }
        }

        private void _Event_EmojiBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            EmojiBorder.BorderBrush = Brushes.Blue;
        }

        private void _Event_EmojiBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            EmojiBorder.BorderBrush = Brushes.Transparent;
        }
    }
}
