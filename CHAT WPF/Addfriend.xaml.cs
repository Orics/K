﻿using System;
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
using System.Windows.Shapes;

namespace CHAT_WPF
{
    /// <summary>
    /// Interaction logic for Addfriend.xaml
    /// </summary>
    public partial class Addfriend : Window
    {
        public Addfriend()
        {
            InitializeComponent();
        }

        private void close_btn_addFriend_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
