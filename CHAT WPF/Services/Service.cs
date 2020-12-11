using Firebase.Storage;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHAT_WPF.Services
{
    public class Service
    {
        public static FirebaseClient Client
        {
            get => new FirebaseClient(new FirebaseConfig()
            {
                AuthSecret = "6zRmtx6gE2vTBgE8kWDsiqgjwCItaJetGK6dkAdg",
                BasePath = "https://kchat-b7025.firebaseio.com/"
            });
        }

        public static FirebaseStorage Storage
        {
            get => new FirebaseStorage("kchat-b7025.appspot.com");
        }

        public static string UserID { get; set; }

    }

    //public class Service
    //{
    //    public static FirebaseClient Client
    //    {
    //        get; set;
    //    }

    //    public static FirebaseStorage Storage
    //    {
    //        get; set;
    //    }

    //    public static string UserID { get; set; }

    //    public Service()
    //    {
    //        Client = new FirebaseClient(new FirebaseConfig()
    //        {
    //            AuthSecret = "6zRmtx6gE2vTBgE8kWDsiqgjwCItaJetGK6dkAdg",
    //            BasePath = "https://kchat-b7025.firebaseio.com/"
    //        });

    //        Storage = new FirebaseStorage("kchat-b7025.appspot.com");
    //    }

    //}
}
