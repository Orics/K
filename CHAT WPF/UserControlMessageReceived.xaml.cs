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

namespace CHAT_WPF
{
    /// <summary>
    /// Interaction logic for UserControlMessageReceived.xaml
    /// </summary>
    public partial class UserControlMessageReceived : UserControl
    {
        public KeyValuePair<string, MessageModel> Model { get; set; }

        public UserControlMessageReceived()
        {
            InitializeComponent();
            this.HorizontalAlignment = HorizontalAlignment.Left;
        }

        public void Load()
        {
            if (Model.Value != null)
            {
                this.Content.Text = Model.Value.Text;
                this.SendTime.Text = KDateTime.ConverToTimeDisplay(Model.Value.SendTime);
            }
        }

        public void Load(KeyValuePair<string, MessageModel> Model)
        {
            this.Model = Model;
            Load();
        }
        private void Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }
    }
}