using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHAT_WPF.Models
{
    public class ConversationModel
    {
        public ConversationModel() { }

        public string ID { get; set; }
        public string Title { get; set; }
        public string Avatar { get; set; }
        public DateTime ChangedTime { get; set; }
        public Dictionary<string, MemberModel> Members { get; set; }
        public Dictionary<string, MessageModel> Messages { get; set; }

    }
}
