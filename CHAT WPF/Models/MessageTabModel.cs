using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHAT_WPF.Models
{
    public  class MessageTabModel
    {
        public KeyValuePair<string, ConversationModel> CurrentConversation { get; set; }
        public Dictionary<string, ConversationModel> Conversations { get; set; }
    }
}
