using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHAT_WPF.Models
{
    public class MessageModel
    {
        public string UserID { get; set; }
        public DateTime SendTime { get; set; }
        public string Text { get; set; }
        public string Sticker { get; set; }
        public List<MessageFileModel> Files { get; set; }
        public List<MessageFileModel> Images { get; set; }

    }
}
