using System.Net.NetworkInformation;
using System.Text;

namespace Nanny
{
    
    public class Server
    {
        private string _name = "";
        public string name
        {
            get
            {
                return _name != "" ? "Name: " + _name : "";
            }
            set => _name = value;
        }

        public int? port;

        private string _ip;

        private IPStatus _status;

        public string _location = "";
        public string location
        {
            get
            {
                return _location != "" ? "Location: " + _location : "";
            }
            set => _location = value;
        }

        public IPStatus status 
        {
            get
            {
                return _status;
            }
            private set => _status = value;
        }

        private string _roundTripTime;
        public string roundTripTime
        {
            get
            {
                return _roundTripTime;
            }
            private set => _roundTripTime = value;
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
            this.name = name;

            options.DontFragment = true;
        }

        public void Ping()
        {
            PingReply reply = pinger.Send(ip, timeOut, buffer, options);
            status = reply.Status;
            roundTripTime = reply.RoundtripTime.ToString();
        }

        public string info
        { 
            get
            {
                Ping();
                var stat = status == IPStatus.Success ? "Success" : "Error";
                return $" {name}\n Host: {ip}\n Status: {stat}\n Roundtrip Time: {roundTripTime} ms\n {location}";
            }
        } 
    }
}
