using System;
using System.Threading.Tasks;

namespace IntuneChatServer.Web
{
    public class HubDbContext : DbContextBase
    {
        private const string connString = @"Server=3622e3bc-2b75-45d8-a892-a5dd00d62baa.sqlserver.sequelizer.com;Database=db3622e3bc2b7545d8a892a5dd00d62baa;User ID=jzralyffsomsvdnu;Password=MpeNFVHcGUEoviTjtvQqTSxHsNJ2uvaUHxdKmj2vgUBSZJomEmGE87z4YCu2mQPm;";

        public HubDbContext()
            : base(connString)
        {
        }
    }
}

