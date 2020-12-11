using CHAT_WPF.Models;
using CHAT_WPF.Services;
using CHAT_WPF.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace CHAT_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MessageTabModel Model { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            InitSystemValues();
        }

        private void InitSystemValues()
        {
            SystemValues.Emojis = ConversationService.GetAllSystemEmojis();
            SystemValues.Stickers = ConversationService.GetAllSystemStickers();
        }

        private void btn_close_mainwindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_mimax_mainwindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void btn_hint_mainwindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Minimized;
        }

        private void DragMove_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {

                //throw;
            }
        }
        private void close_kchat_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenUpdateAccountInfor(object sender, MouseButtonEventArgs e)
        {
            new InforUpdate().Show();
        }

        private void _Event_LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Utilities.Ultilities.SaveUserInfor("", "", "");
            MessageBox.Show("Logout");
            new login().Show();
            this.Close();
        }
    }
}
