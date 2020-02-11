using Hybrid.Quartz.Dashboard.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hybrid.Quartz.Dashboard.Repositorys
{
    public static class ServerRepository
    {
        private static List<Server> _servers;

        static ServerRepository()
        {
            Initialize();
        }

        private static void Initialize()
        {
            _servers = new List<Server> { new Server("localhost", "http://localhost:28682/") };
        }

        public static Server Lookup(string name)
        {
            return _servers.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public static IReadOnlyList<Server> LookupAll()
        {
            return _servers;
        }
    }
}