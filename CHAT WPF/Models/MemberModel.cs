using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHAT_WPF.Models
{
    
    public class MemberModel
    {
        public struct Statuses
        {
            public static string Joined = "Joined";
            public static string Invited = "Invited";
        }

        public bool IsEntering { get; set; }
        public string Status { get; set; }
        public DateTime SeenTime { get; set; }
    }
}
