using CHAT_WPF.Models;
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
    /// Interaction logic for SystemMesageControl.xaml
    /// </summary>
    public partial class SystemMesageControl : UserControl
    {
        public KeyValuePair<string, MessageModel> Model { get; set; }

        public SystemMesageControl(KeyValuePair<string, MessageModel> model)
        {
            InitializeComponent();
            Model = model;
            Load();
        }

        public void Load()
        {
            if(Model.Value != null)
            {
                this.MessageContent.Text = Model.Value.Text;
            }
        }
    }
}
