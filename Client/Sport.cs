using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.Data.Json;
using Windows.UI.Xaml;

namespace testui
{
    class Sport
    {
        public JsonObject Name { get => name; }
        private JsonObject name;
        private Server server = new Server();

        public async Task initial()
        {
            JsonObject.TryParse(await server.get("type=sportname"), out name);
        }

        public async Task<bool> addSport(string sport)
        {
            string res = await server.get($"type=sportadd&sport={Useful.Fa_En(sport)}");
            return res == "1";
        }
        public async Task<bool> addLeague(string sport, string league)
        {
            string res = await server.get($"type=leagueadd&sport={Useful.Fa_En(sport)}&league={Useful.Fa_En(league)}&info=$isleague$:true");
            return res == "1";
        }

        public async Task<StackPanel[]> Rank(string sport, string league)
        {
            JsonObject j;
            JsonObject.TryParse(await server.get($"type=leaguerank&sport={sport}&league={league}"), out j);
            JsonArray rank = j["rank"].GetArray();
            StackPanel[] result = new StackPanel[/*rank.Count*/10];
            

            for (int i = 0; i < /*rank.Count*/10; i++)
            {
                result[i] = new StackPanel { Orientation = Orientation.Horizontal };
                result[i].Children.Add(new TextBlock { Text = "#" });
                //result[i].Children.Add(new TextBlock { Text = rank.GetStringAt(i), Margin = new Thickness(12, 0, 12, 0) });
                result[i].Children.Add(new TextBlock { Text = "امتیاز" });
                result[i].Children.Add(new TextBlock { Text = "برد", Margin = new Thickness(12, 0, 12, 0) });
                result[i].Children.Add(new TextBlock { Text = "باخت" });
            }
            return result;
        }
    }
}
