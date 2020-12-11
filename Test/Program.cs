using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = ReadImage("https://firebasestorage.googleapis.com/v0/b/kchat-b7025.appspot.com/o/-MKUEQQTzXFSkOM04Fr1%2F2.PNG?alt=media&token=5257a754-c706-4dfd-a310-8de6ac4069e6");
            
        }

        public static async Task ReadImage(string url)
        {
            using (WebClient client = new WebClient())
            {
                await client.OpenReadTaskAsync(url);
            }
        }
    }
}
