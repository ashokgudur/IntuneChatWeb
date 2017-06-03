using IntuneChat.Model;
using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IntuneChatServer.Web
{
    public class ChatHub : Hub
    {
        public void SendComment(ChatMessage chatMessage)
        {
            IList<string> connectionIds = null;

            var uc = new UserContext();
            if (chatMessage.CommentType == CommentType.Contact)
                connectionIds = uc.GetUserConnectionIds(chatMessage.ToEmail);
            else
                connectionIds = uc.GetAccountUserConnectionIds(chatMessage);

            Clients.Clients(connectionIds).addComment(chatMessage);
        }

        public override Task OnConnected()
        {
            adduserConnection();
            return base.OnConnected();
        }

        private void adduserConnection()
        {
            var email = Context.Headers["UserEmail"];
            var name = Context.Headers["UserName"];

            var userConnection = new UserConnection
            {
                ConnectionId = Context.ConnectionId,
                Email = email,
                Name = name,
            };

            var uc = new UserContext();
            uc.AddUserConnection(userConnection);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var uc = new UserContext();
            uc.DeleteUserConnection(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            //var uc = new UserContext();
            //uc.DeleteUserConnection(Context.ConnectionId);
            //adduserConnection();

            //TODO: What to update?
            return base.OnReconnected();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
