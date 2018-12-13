using System.Collections.Generic;

namespace IntuneChatServer.Web
{
    public class HubUserDao
    {
        DbContextBase _dbc = null;
        UserConnection _userConnection = null;

        public HubUserDao(DbContextBase dbc, UserConnection userConnection)
            : this(dbc)
        {
            _userConnection = userConnection;
        }

        public HubUserDao(DbContextBase dbc)
        {
            _dbc = dbc;
        }

        public void AddUserConnection()
        {
            var sql = "insert into [UserConnection] (name, email, connectionId) values (@userName, @userEmail, @connectionId)";

            var cmd = _dbc.CreateCommand(sql);
            _dbc.AddParameterWithValue(cmd, "@userName", _userConnection.Name);
            _dbc.AddParameterWithValue(cmd, "@userEmail", _userConnection.Email);
            _dbc.AddParameterWithValue(cmd, "@connectionId", _userConnection.ConnectionId);
            cmd.ExecuteNonQuery();

            _userConnection.Id = _dbc.GetGeneratedIdentityValue();
        }

        public void DeleteUserConnection(string connectionId)
        {
            var sql = "delete from [UserConnection] where connectionId=@connectionId";
            var cmd = _dbc.CreateCommand(sql);
            _dbc.AddParameterWithValue(cmd, "@connectionId", connectionId);
            cmd.ExecuteNonQuery();
        }

        public List<string> GetUserConnectionIds(string emails)
        {
            var sql = string.Format("select connectionId from [UserConnection] where email in('{0}')", emails);
            var cmd = _dbc.CreateCommand(sql);
            var rdr = cmd.ExecuteReader();
            var result = new List<string>();

            while (rdr.Read())
                result.Add(rdr["connectionId"].ToString());

            return result;
        }
    }
}

