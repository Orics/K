using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHAT_WPF.Models
{
    public class MessageUploadFileModel
    {
        public string ConversationID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string DowloadUrl { get; set; }

        public MessageFileModel ConvertToMessageFileModel()
        {
            return new MessageFileModel()
            {
                ConversationID = ConversationID,
                FileName = FileName,
                DowloadUrl = DowloadUrl,
            };
        }
    }
}
