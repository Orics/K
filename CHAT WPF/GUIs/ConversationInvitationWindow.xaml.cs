using CHAT_WPF.Models;
using CHAT_WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CHAT_WPF.GUIs
{
    /// <summary>
    /// Interaction logic for ConversationInvitationWindow.xaml
    /// </summary>
    public partial class ConversationInvitationWindow : Window
    {
        public Dictionary<string, UserModel> Users { get; set; }
        public Dictionary<string, UserModel> InvitedUsers { get; set; }
        public Dictionary<string, MemberModel> Members { get; set; }
        public KeyValuePair<string, ConversationModel> Model { get; set; }

        public ConversationInvitationWindow(KeyValuePair<string, ConversationModel> model)
        { 
            InitializeComponent();
            Model = model;
            Load();
        }

        public void Load()
        {
            Members = Model.Value.Members;

            Users = UserService.GetAllUsers();

            InvitedUsers = new Dictionary<string, UserModel>();

            foreach (var user in Users)
            {
                if (!IsInListInvitedUsers(user.Key))
                {
                    if (IsJoinedMember(user.Key))
                    {
                        SearchResultsContainer.Children.Add(new ConversationInvitationItemControl(this, user, MemberModel.Statuses.Joined));
                    }
                    else
                    if (IsInvitedMember(user.Key))
                    {
                        SearchResultsContainer.Children.Add(new ConversationInvitationItemControl(this,user, MemberModel.Statuses.Invited));
                    }
                    else
                    {
                        SearchResultsContainer.Children.Add(new ConversationInvitationItemControl(this, user));
                    }
                }
                
            }
        }

        private bool IsJoinedMember(string userid)
        {
            if (Members.ContainsKey(userid))
            {
                MemberModel member;
                Members.TryGetValue(userid, out member);
                if(member!=null && member.Status == MemberModel.Statuses.Joined)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsInvitedMember(string userid)
        {
            if (Members.ContainsKey(userid))
            {
                MemberModel member;
                Members.TryGetValue(userid, out member);
                if (member != null && member.Status == MemberModel.Statuses.Invited)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsInListInvitedUsers(string userid)
        {
            if(InvitedUsers != null && InvitedUsers.ContainsKey(userid))
            {
                return true;
            }
            return false;
        }

        private void close_btn_invite_friend_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
