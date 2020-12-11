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
using System.Windows.Shapes;

namespace CHAT_WPF
{
    /// <summary>
    /// Interaction logic for InforUpdate.xaml
    /// </summary>
    public partial class InforUpdate : Window
    {
        public InforUpdate()
        {
            InitializeComponent();
        }

        private void close_btn_infoUpdate_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
