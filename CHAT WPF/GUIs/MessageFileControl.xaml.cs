using CHAT_WPF.Models;
using CHAT_WPF.Services;
using CHAT_WPF.Utilities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for MessageFileControl.xaml
    /// </summary>
    public partial class MessageFileControl : UserControl
    {
        public MessageFileModel Model { get; set; }

        public MessageFileControl(MessageFileModel model)
        {
            InitializeComponent();
            Model = model;
            Load();
        }

        public MessageFileControl(MessageUploadFileModel model)
        {
            InitializeComponent();
            Load(model);
        }

        public void Load()
        {
            if(Model != null)
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

                // StorageService.DowloadFileAsync(Model);
                FileNameTextBlock.Text = Model.FileName;
            } 
        }

        public void Load(MessageUploadFileModel model)
        {
            if (model != null)
            {
                Model = model.ConvertToMessageFileModel();
                Load();
            }
            
        }

        private void _Event_DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            if (FileTab.Instance != null && Model != null && Model.DowloadUrl != null)
            {
                FileTab.Instance.FileRecentContainer.Children.Add(new FileItemControl(Model));
            }
            else
            {
                MessageBox.Show("NULL");
            }
        }
    }
}
