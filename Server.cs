using System.Net.NetworkInformation;
using System.Text;

namespace Nanny
{
    
    public class Server
    {
        public string Name { get; set; } = "";

        public int? port;

        private string _ip;

        private IPStatus _status;

        public IPStatus status 
        {
            get
            {
                return _status;
            }
            private set => _status = value;
        }

        public string ip
        {
            get
            {
                return port != null ? _ip + ':' + port.ToString() : _ip;
            }
            set => _ip = value;
        }

        Ping pinger = new Ping();
        PingOptions options = new PingOptions();
        string pingData = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"; // 32 bytes

        byte[] buffer => Encoding.ASCII.GetBytes(pingData);
        public int timeOut { get; set; } = 120;

        public Server(string ip, int? port = null, string name = "")
        {
            this._ip = ip;
            this.port = port;
            this.Name = name;

            options.DontFragment = true;
        }

        public void Ping()
        {
            PingReply reply = pinger.Send(ip, timeOut, buffer, options);
            status = reply.Status;
        }

        public string info
        { 
            get
            {
                Ping();
                var stat = status == IPStatus.Success ? "Success" : "Error";
                return $"Host: {ip}" + ' ' + $"Status: {stat}";
            }
        } 
    }
}
