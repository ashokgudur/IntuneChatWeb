using IntuneChat.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace IntuneChatServer.Web
{
    public class UserContext
    {
        public IList<string> GetUserConnectionIds(string email)
        {
            using (HubDbContext dbc = new HubDbContext())
            {
                var dao = new HubUserDao(dbc);
                return dao.GetUserConnectionIds(email);
            }
        }

        public IList<string> GetAccountUserConnectionIds(ChatMessage chatMessage)
        {
            string[] emails = null;

            using (UserDbContext dbc = new UserDbContext())
            {
                var dao = new UserDao(dbc);
                emails = dao.GetAccountUsersEmails(chatMessage);
            }

            using (HubDbContext dbc = new HubDbContext())
            {
                var emailsString = string.Join("','", emails);
                var dao = new HubUserDao(dbc);
                return dao.GetUserConnectionIds(emailsString);
            }
        }

        public void AddUserConnection(UserConnection userConnection)
        {
            //TODO: the user is validated from Intune database.
            // up on not finding it we skip to add into the active connections.
            // Find a better way to do this. 
            // Currently we don't have SignalR support for refusing connections

            using (UserDbContext dbc = new UserDbContext())
            {
                var dao = new UserDao(dbc, userConnection);
                if (!dao.IsUserExists())
                    return;
            }

            using (HubDbContext dbc = new HubDbContext())
            {
                var dao = new HubUserDao(dbc, userConnection);
                dao.AddUserConnection();
            }
        }

        public void DeleteUserConnection(string connectionId)
        {
            using (HubDbContext dbc = new HubDbContext())
            {
                var dao = new HubUserDao(dbc);
                dao.DeleteUserConnection(connectionId);
            }
        }
    }
}

