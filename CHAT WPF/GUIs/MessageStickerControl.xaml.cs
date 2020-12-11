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
    /// Interaction logic for MessageStickerControl.xaml
    /// </summary>
    public partial class MessageStickerControl : UserControl
    {
        public MessageStickerControl(string source)
        {
            InitializeComponent();
            ImageBehavior.SetAnimatedSource(Sticker, Ultilities.ConvertBase64StringToBitmapImage(source));
        }
    }
}
