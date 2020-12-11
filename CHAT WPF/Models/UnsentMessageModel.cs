using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHAT_WPF.Models
{
    public class UnsentMessageModel
    {
        public string UserID { get; set; }
        public string ConversationID { get; set; }
        public string Text { get; set; }
        public string Sticker { get; set; }
        public List<MessageUploadFileModel> Files { get; set; }
        public List<MessageUploadFileModel> Images { get; set; }

        public MessageModel ConvertToMessageModel()
        {
            List<MessageFileModel> images = null;
            if (Images != null)
            {
                images = new List<MessageFileModel>();
                foreach (MessageUploadFileModel item in Images)
                {
                    images.Add(item.ConvertToMessageFileModel());
                }
            }


            List<MessageFileModel> files = null;
            if (Files != null)
            {
                files = new List<MessageFileModel>();
                foreach (MessageUploadFileModel item in Files)
                {
                    files.Add(item.ConvertToMessageFileModel());
                }
            }


            return new MessageModel()
            {
                UserID = UserID,
                Text = Text,
                SendTime = DateTime.Now,
                Sticker = Sticker,
                Files = files,
                Images = images,
            };
        }
    }
}
