namespace Hybrid.Quartz.Dashboard.Models
{
    public class Server
    {
        public Server(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public string Name { get; private set; }
        public string Address { get; private set; }
    }
}