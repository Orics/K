using CHAT_WPF.Models;
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
    /// Interaction logic for ConversationUserEnteringControl.xaml
    /// </summary>
    public partial class ConversationUserEnteringControl : UserControl
    {
        public UserModel Model { get; set; }
        public ConversationUserEnteringControl(UserModel model)
        {
            InitializeComponent();
            Model = model;
            Load();
        }

        public void Load()
        {
            if(Model!= null)
            {
                UserAvatarImage.ImageSource = Ultilities.ConvertBase64StringToBitmapImage(Model.Avatar);
                UserAvatarBorder.ToolTip = new ToolTip() { Content = Model.Fullname };
            }
        }
    }
}
