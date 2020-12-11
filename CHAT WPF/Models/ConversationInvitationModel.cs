using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHAT_WPF.Models
{
    public class ConversationInvitationModel
    {
        public string ConversationID { get; set; }
        public string FromUserID { get; set; }
        public string ToUserID { get; set; }
    }
}
