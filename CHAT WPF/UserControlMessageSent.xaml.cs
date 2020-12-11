using CHAT_WPF.GUIs;
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
    /// Interaction logic for UserControlMessageSent.xaml
    /// </summary>
    public partial class UserControlMessageSent : UserControl
    {
        public KeyValuePair<string, MessageModel> Model { get; set; }

        public UserControlMessageSent(KeyValuePair<string, MessageModel> model)
        {
            InitializeComponent();
            this.HorizontalAlignment = HorizontalAlignment.Right;

            Model = model;
            Load();
        }

        private void Load()
        {
            if (Model.Value != null)
            {
                this.Content.Text = Model.Value.Text;

                if (Model.Value.Images != null)
                {
                    foreach (MessageFileModel file in Model.Value.Images)
                    {
                        this.MessageImagesContainer.Children.Add(new MessageImageControl(file));
                    }
                }

                if (Model.Value.Files != null)
                {
                    foreach (MessageFileModel file in Model.Value.Files)
                    {
                        this.MessageFilesContainer.Children.Add(new MessageFileControl(file));
                    }
                }
            }
        }

        public void Load(KeyValuePair<string, MessageModel> Model)
        {
            this.Model = Model;
            Load();
        }


    }
}
