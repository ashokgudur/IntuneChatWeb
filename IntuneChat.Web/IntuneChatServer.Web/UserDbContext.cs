namespace IntuneChatServer.Web
{
    public class UserDbContext : DbContextBase
    {
        private const string connString = @"Server=3622e3bc-2b75-45d8-a892-a5dd00d62baa.sqlserver.sequelizer.com;Database=db3622e3bc2b7545d8a892a5dd00d62baa;User ID=jzralyffsomsvdnu;Password=i27HrvMAWiiEmJyJG5doc7AtQ4xGU8vTHNchtwharnmsWQ8hk4j4erVkzMZw7fPX;";

        public UserDbContext()
            : base(connString)
        {
        }
    }
}

