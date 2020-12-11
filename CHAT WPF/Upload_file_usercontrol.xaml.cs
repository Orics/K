using CHAT_WPF.Models;
using CHAT_WPF.Services;
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

namespace CHAT_WPF
{
    /// <summary>
    /// Interaction logic for Upload_file_usercontrol.xaml
    /// </summary>
    public partial class Upload_file_usercontrol : UserControl
    {
        public MessageUploadFileModel Model { get; set; }

        public Upload_file_usercontrol(MessageUploadFileModel model)
        {
            InitializeComponent();
            Model = model;
            UploadFileAsync();
        }

        private async Task UploadFileAsync()
        {
            
            var task = Service.Storage.Child(Model.ConversationID)
                                      .Child(Model.FileName)
                                      .PutAsync(File.OpenRead(Model.FilePath));
            
            task.Progress.ProgressChanged += (s, e) => { MaterialDesignThemes.Wpf.ButtonProgressAssist.SetValue(UploadProcess, e.Percentage);};

            FilenameTextBlock.Text = Model.FileName;

            Model.DowloadUrl = await task;

            UploadProcess.Visibility = Visibility.Hidden;
        }

        private void RemoveFile()
        {
            var task = Service.Storage.Child(Model.ConversationID)
                                      .Child(Model.FileName)
                                      .DeleteAsync();
        }

        private  bool CheckFileDulicate(string filename)
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
