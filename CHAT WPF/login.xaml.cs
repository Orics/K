using CHAT_WPF.Models;
using CHAT_WPF.Services;
using Firebase.Auth;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        private bool running = false;

        public login()
        {
            InitializeComponent();

            LoginModel login = Utilities.Ultilities.ReadUserInfor();
            if (login != null)
            {
                new UserService();

                var user = UserService.Login(login.Username, login.Password);
                if (user != null)
                {
                    new MainWindow().Show();
                    this.Close();
                }
            }
        }


        //private async void UploadFiles()
        //{

        //    var stream = File.Open("D:\\non-male.png", FileMode.Open);
        //   // var auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyAASNSON5MpWzcg7Dol_UDEWfMJQqvGsuE"));
        //    //var a = await auth.SignInWithEmailAndPasswordAsync("nguyenngockhai25@gmail.com", "Orics@1011f337");

        //    var cancellation = new CancellationTokenSource();
        //    var task = new FirebaseStorage(
        //        "kchat-b7025.appspot.com"
        //     )
        //    .Child("data")
        //    .Child("Capture.png")
        //    .PutAsync(stream, cancellation.Token);
        //    task.Progress.ProgressChanged += (s, e) => Username.Text = ($"Progress: {e.Percentage} %");

        //    // cancel the upload
        //    // cancellation.Cancel();

        //    try
        //    {
        //        // error during upload will be thrown when you await the task
        //        Username.Text = ("link:\n" + await task);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}


        //==========================================================LOGIN============================================================================

        private void Login(object sender, RoutedEventArgs e)
        {
            var username = this.Username.Text;
            var password = this.Password.Password;

            new UserService();

            var user = UserService.Login(username, password);
            if (user != null)
            {
                Utilities.Ultilities.SaveUserInfor(Service.UserID, username, password);
                this.Hide();
                new MainWindow().Show();

            }

        }




        //==========================================================REGISTER============================================================================
        private bool CheckValidate()
        {
            if (!string.IsNullOrEmpty(R_Username.Text))
            {
                if (!UserService.CheckUsernameDuplicate(R_Username.Text))
                {
                    if (!string.IsNullOrEmpty(R_Password.Password))
                    {
                        if (!string.IsNullOrEmpty(R_Phone.Text))
                        {
                            if (!string.IsNullOrEmpty(R_Email.Text))
                            {
                                return true;
                            }
                            else
                            {
                                MessageBox.Show("Email khong hop le");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Phone khong hop le");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Password khong hop le");
                    }
                }
                else
                {
                    MessageBox.Show("Username was duplicated");
                }
            }
            else
            {
                MessageBox.Show("Username khong hop le");
            }
            return false;
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Đăng ký !");
            new UserService();
            if (CheckValidate())
            {
                UserService.Register(new UserModel()
                {
                    Username = R_Username.Text,
                    Fullname = R_Fullname.Text,
                    Password = R_Password.Password,
                    Phone = R_Phone.Text,
                    Email = R_Email.Text
                });
            }
        }

        private void close_login_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void dragMe(object sender, MouseButtonEventArgs e)
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
       
    }
}
