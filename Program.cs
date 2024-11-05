using Nanny;
using Newtonsoft.Json;

namespace nanny
{
    class Program
    {
        static Instance instance;

        [JsonConverter(typeof(ServerConverter))]
        static List<Server> servers;

        static void Main(string[] args)
        {
            ServerConverter serverConverter = new ServerConverter();
            instance = new Instance(servers);
            mainThread.Start();
        }

        public static void InitServersList()
        {
            string jsonloc = System.AppContext.BaseDirectory + "Servers.json";
            using StreamReader r = new StreamReader(jsonloc);
            string jsontext = r.ReadToEnd();

            servers = JsonConvert.DeserializeObject<List<Server>>(jsontext);

        }

        static Thread mainThread = new(() => // "gameloop"
        {
            while (true)
            {
                InitServersList();
                instance.Update(servers);
                Thread.Sleep(100);
            }
        });
    }
}