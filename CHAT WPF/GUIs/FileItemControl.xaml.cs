using CHAT_WPF.Models;
using CHAT_WPF.Services;
using CHAT_WPF.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Windows.Threading;

namespace CHAT_WPF.GUIs
{
    /// <summary>
    /// Interaction logic for FileItemControl.xaml
    /// </summary>
    public partial class FileItemControl : UserControl
    {
        public MessageFileModel Model { get; set; }

        public FileItemControl(MessageFileModel model)
        {
            InitializeComponent();
            Model = model;
            Load();
        }

        public void Load()
        {
            if(Model!= null)
            {
                string fileExtension = System.IO.Path.GetExtension(Model.FileName);
                string fileIconPath = Ultilities.GetRootPath() + "\\Asset\\FileIcons\\" + fileExtension + ".png";
                string unknownIconPath = Ultilities.GetRootPath() + "\\Asset\\FileIcons\\unknown.png";

                if (System.IO.File.Exists(fileIconPath))
                {
                    FileIcon.ImageSource = new BitmapImage(new Uri(fileIconPath));
                }
                else
                {
                    FileIcon.ImageSource = new BitmapImage(new Uri(unknownIconPath));
                }

                FileName.Text = Model.FileName;

                DowloadedTime.Text = DateTime.Now.ToString();

                using (WebClient client = new WebClient())
                {
                    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler((sender, e) => {
                        Dispatcher.InvokeAsync(() => {
                            MaterialDesignThemes.Wpf.ButtonProgressAssist.SetValue(Loading, e.ProgressPercentage);
                            Console.WriteLine(e.ProgressPercentage);
                            if (e.ProgressPercentage >= 100)
                            {
                                Loading.Visibility = Visibility.Collapsed;
                                FileSize.Visibility = Visibility.Visible;
                                FileSize.Text = (e.BytesReceived / (1024 * 1024)).ToString() + " MB";
                            }
                        }, DispatcherPriority.ApplicationIdle);  
                    });   
                    
                    client.DownloadFileTaskAsync(Model.DowloadUrl, @"C:\Downloads Kchat\" + Model.FileName);
                }
            }
        }

        private void _Event_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.InvokeAsync(() => {
                Load();
            }, DispatcherPriority.ApplicationIdle);
        }
    }
}
