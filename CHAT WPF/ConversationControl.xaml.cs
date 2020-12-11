using CHAT_WPF.GUIs;
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

namespace CHAT_WPF
{
    /// <summary>
    /// Interaction logic for ConversationBoxControl.xaml
    /// </summary>
    public partial class ConversationControl : UserControl
    {

        public KeyValuePair<string, ConversationModel> Model { get; set; }

        public ConversationTabControl ConversationTab { get; set; }

        public ConversationControl()
        {
            InitializeComponent();
        }

        public void Load() { 
            if(Model.Value != null)
            {
                this.Title.Text = Model.Value.Title;
                //--------------------------
            }
        }

        public void Load(KeyValuePair<string, ConversationModel> model)
        {
            if (Model.Value != null)
            {
                this.Title.Text = Model.Value.Title;
                //--------------------------
            }
        }

        public void ShowConversation()
        {
            if (ConversationTab != null && Model.Value != null)
            {
               // ConversationTab.OpenConversation(Model.Key);
            }

        }

        private void _EventClick(object sender, MouseButtonEventArgs e)
        {
            ShowConversation();
        }

        private void _EventLoaded(object sender, RoutedEventArgs e)
        {
            Load();
        }
    }
}
