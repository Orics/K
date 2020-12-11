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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CHAT_WPF.GUIs
{
    /// <summary>
    /// Interaction logic for UploadFileControl.xaml
    /// </summary>
    public partial class UploadFileControl : UserControl
    {
        public MessageUploadFileModel Model { get; set; }

        public UploadFileControl(MessageUploadFileModel model)
        {
            InitializeComponent();
            Model = model;
            Load();
            UploadFileAsync();
        }

        private void Load()
        {
            if (Model != null)
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
                FileName.Text = Model.FileName;
            }
        }

        private async Task UploadFileAsync()
        {
            var task = Service.Storage.Child(Model.ConversationID)
                                      .Child(Model.FileName)
                                      .PutAsync(File.OpenRead(Model.FilePath));

            task.Progress.ProgressChanged += (s, e) => { MaterialDesignThemes.Wpf.ButtonProgressAssist.SetValue(UploadProcess, e.Percentage); };

            FileName.Text = Model.FileName;

            Model.DowloadUrl = await task;

            if (!string.IsNullOrEmpty(Model.DowloadUrl))
            {
                UploadProcess.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Upload failed");
                // remove file from firebase
                RemoveFile();
                // remove control 
                ((Panel)this.Parent).Children.Remove(this);
            }

            
        }

        private void RemoveFile()
        {
            var task = Service.Storage.Child(Model.ConversationID)
                                      .Child(Model.FileName)
                                      .DeleteAsync();
        }

        private bool CheckFileDulicate(string filename)
        {
            return false;

        }

        private void _Event_CancelButton_Clicked(object sender, RoutedEventArgs e)
        {
            // remove file from firebase
            RemoveFile();

            // remove control 
            ((Panel)this.Parent).Children.Remove(this);

        }
    }
}
