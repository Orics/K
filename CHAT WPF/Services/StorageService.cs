using CHAT_WPF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CHAT_WPF.Services
{
    public class StorageService : Service
    {
        public StorageService(): base() { }

        public static Task<Stream> GetFileAsync(MessageFileModel response)
        {
            using (WebClient client = new WebClient())
            {
                 return client.OpenReadTaskAsync(response.DowloadUrl);
            }
        }

        public static WebClient DowloadFileAsync(MessageFileModel response, string savepath = @"D:\Test\")
        {
            using (WebClient client = new WebClient())
            {
                //client.DownloadProgressChanged += new DownloadProgressChangedEventHandler((sender, e) => Console.WriteLine(e.ProgressPercentage));
                client.DownloadFileTaskAsync(response.DowloadUrl, savepath + response.FileName);
                return client;
            }
        }
    }
}
