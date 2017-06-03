using IntuneChat.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntuneChatServer.Web
{
    public class UserDao
    {
        UserDbContext _dbc = null;
        UserConnection _userConnection = null;

        public UserDao(UserDbContext dbc, UserConnection userConnection)
            : this(dbc)
        {
            _userConnection = userConnection;
        }

        public UserDao(UserDbContext dbc)
        {
            _dbc = dbc;
        }

        public string[] GetAccountUsersEmails(ChatMessage chatMessage)
        {
            var result = new List<string>();
            var sql = string.Format("select u.email from AccountUser au inner join [User] u on au.userId = u.id where accountId={0} and u.email<>'{1}'", chatMessage.AccountId, chatMessage.ByEmail);
            var rdr = _dbc.ExecuteReader(sql);
            while (rdr.Read())
                result.Add(rdr["email"].ToString());

            rdr.Close();
            return result.ToArray();
        }

        public bool IsUserExists()
        {
            //TODO: Add additional checks for existence of Mobile, AtUserName also
            var sql = "select count(id) from [User] where email=@userEmail";
            var cmd = _dbc.CreateCommand(sql);
            _dbc.AddParameterWithValue(cmd, "@userEmail", _userConnection.Email);
            var result = cmd.ExecuteScalar();
            var idCcount = Convert.ToInt32(result);
            return idCcount > 0;
        }
    }
}

