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
        public async Task initial2()
        {
            JsonObject.TryParse(await server.get("type=sportactivename"), out name);
        }

        public async Task<bool> addSport(string sport)
        {
            string res = await server.get($"type=sportadd&sport={Useful.Fa_En(sport)}");
            return res == "1";
        }
        public async Task<bool> addLeague(string sport, string league, int teamNumber, int memberNumber)
        {
            return await server.get($"type=leagueadd&sport={Useful.Fa_En(sport)}&league={Useful.Fa_En(league)}&info=$isleague$:true,$nteam$:{teamNumber},$nmember$:{memberNumber}") == "1";
        }
        public async Task<bool> activeleague(string sport, string league)
        {
            return (await server.get($"type=leagueactive&sport={Useful.Fa_En(sport)}&league={Useful.Fa_En(league)}") == "1");
        }
        public async Task<bool> inactiveleague(string sport, string league)
        {
            return (await server.get($"type=leaguedelete&sport={Useful.Fa_En(sport)}&league={Useful.Fa_En(league)}") == "1");
        }
        public async Task<bool> endleague(string sport, string league)
        {
            return (await server.get($"type=leagueend&sport={Useful.Fa_En(sport)}&league={Useful.Fa_En(league)}") == "1");
        }
        public async Task<StackPanel[]> Rank(string sport, string league)
        {
            JsonObject j;
            JsonObject.TryParse(await server.get($"type=leaguerank&sport={Useful.Fa_En(sport)}&league={Useful.Fa_En(league)}"), out j);
            JsonArray rank = j["rank"].GetArray();
            StackPanel[] result = new StackPanel[rank.Count];
            

            for (int i = 0; i < rank.Count; i++)
            {
                string teamname = rank.GetStringAt(Convert.ToUInt32(i));
                JsonObject team = j["team"].GetObject()[teamname].GetObject();
                result[i] = new StackPanel { Orientation = Orientation.Horizontal, FlowDirection=FlowDirection.RightToLeft };

                //#    name   
                result[i].Children.Add(new TextBlock { Text = (i + 1).ToString() }); 
                result[i].Children.Add(new TextBlock { Text = teamname, Margin = new Thickness(12, 0, 12, 0) });

                switch (sport)
                {
                    case "clp ba DFAgl"://h1    h2    h3    fault
                        result[i].Children.Add(new TextBlock { Text = team["h1"].GetNumber().ToString() });
                        result[i].Children.Add(new TextBlock { Text = team["h2"].GetNumber().ToString(), Margin = new Thickness(12, 0, 12, 0) });
                        result[i].Children.Add(new TextBlock { Text = team["h3"].GetNumber().ToString() });
                        result[i].Children.Add(new TextBlock { Text = team["fault"].GetNumber().ToString(), Margin = new Thickness(12, 0, 12, 0) });
                        break;
                    case "wDdbaA jodF CabFCaFaC":
                        result[i].Children.Add(new TextBlock { Text = team["score"].GetNumber().ToString() });
                        result[i].Children.Add(new TextBlock { Text = team["win"].GetNumber().ToString(), Margin = new Thickness(12, 0, 12, 0) });
                        result[i].Children.Add(new TextBlock { Text = team["lost"].GetNumber().ToString() });
                        result[i].Children.Add(new TextBlock { Text = team["equal"].GetNumber().ToString(), Margin = new Thickness(12, 0, 12, 0) });
                        result[i].Children.Add(new TextBlock { Text = team["wg"].GetNumber().ToString() });
                        result[i].Children.Add(new TextBlock { Text = team["lg"].GetNumber().ToString(), Margin = new Thickness(12, 0, 12, 0) });
                        result[i].Children.Add(new TextBlock { Text = (team["wg"].GetNumber() - team["lg"].GetNumber()).ToString() });
                        break;
                    case "yaCdl pbyE":
                        result[i].Children.Add(new TextBlock { Text = team["score"].GetNumber().ToString() });
                        result[i].Children.Add(new TextBlock { Text = team["win"].GetNumber().ToString(), Margin = new Thickness(12, 0, 12, 0) });
                        result[i].Children.Add(new TextBlock { Text = team["lost"].GetNumber().ToString() });
                        result[i].Children.Add(new TextBlock { Text = team["equal"].GetNumber().ToString(), Margin = new Thickness(12, 0, 12, 0) });
                        result[i].Children.Add(new TextBlock { Text = team["wk"].GetNumber().ToString() });
                        result[i].Children.Add(new TextBlock { Text = team["lk"].GetNumber().ToString(), Margin = new Thickness(12, 0, 12, 0) });
                        result[i].Children.Add(new TextBlock { Text = (team["wk"].GetNumber() - team["lk"].GetNumber()).ToString() });
                        break;
                    default:
                        break;
                }
            }
            return result;
        }
    }
}
