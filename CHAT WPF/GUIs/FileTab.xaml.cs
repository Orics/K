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
    /// Interaction logic for FileTab.xaml
    /// </summary>
    public partial class FileTab : UserControl
    {

        private static FileTab _instance;

        public static FileTab Instance {
            get
            {
                if (_instance == null)
                {
                    return new FileTab();
                }
                else
                {
                    //DEFAULT value here. 
                    return _instance;
                }
            }
            set
            {
                _instance = value;
            }        
        }

        public FileTab()
        {
            InitializeComponent();
            _instance = this;
        }
    }
}
