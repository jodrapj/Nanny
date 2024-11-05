using Spectre.Console;
using Spectre.Console.Rendering;

namespace Nanny
{
    public class Instance
    {
        public Panel MainPanel { get; private set; }

        private List<Text>[] rows = new List<Text>[3];
        Layout mainLayout;

        public Instance(List<Server>? servers = null)
        {
            if (System.OperatingSystem.IsOSPlatform("windows"))
            {
                Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            }
            if (servers != null)
            {
                SetupLayout(servers);
            }
            SetupMainpanel(mainLayout);
            AnsiConsole.Write(MainPanel);
        }

        private void SetupMainpanel(IRenderable content = null)
        {
            if (content == null)
            {
                content = new Text("");
            }
            MainPanel = new Panel(content);
            MainPanel.Header = new PanelHeader("Nanny v0.1", Justify.Center);
            MainPanel.Height = Console.WindowHeight - 1;
            MainPanel.Width = Console.WindowWidth;
        }

        private void SetupLayout(List<Server> servers)
        {

            int counter = 0;

            for (int l = 0; l < 3; l++)
            {
                rows[l] = new List<Text>();
            }

            for (int i = 0; i < servers.Count; i++)
            {

                Style style = new Style(servers[i].info.Contains("Error") ? Color.Red : Color.Green);

                int arrayIndex = i % rows.Length;
                int elementIndex = i / rows.Length;

                rows[arrayIndex].Add(new Text(servers[i].info, style));
            }


            mainLayout = new Layout()
                .SplitColumns(
                new Layout(renderable: new Panel(new Rows(rows[0]))),
                new Layout(renderable: new Panel(new Rows(rows[1]))
                {
                    Border = rows[1].Count > 0 ? BoxBorder.Square : BoxBorder.None,
                }),
                new Layout(renderable: new Panel(new Rows(rows[2]))
                {
                    Border = rows[2].Count > 0 ? BoxBorder.Square : BoxBorder.None,
                })
                );
        }

        public void Update(List<Server>? servers = null)
        {
            if (servers != null)
            {
                //SetupServerPanel(servers);
                SetupLayout(servers);
            }
            SetupMainpanel(mainLayout);
            AnsiConsole.Cursor.SetPosition(0, 0);
            AnsiConsole.Write(MainPanel);
        }
    }
}
