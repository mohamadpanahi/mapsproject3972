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
        public async Task<bool> addLeague(string sport, string league, int teamNumber)
        {
            return await server.get($"type=leagueadd&sport={Useful.Fa_En(sport)}&league={Useful.Fa_En(league)}&info=$isleague$:true,$nteam$:{teamNumber}") == "1";
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
            StackPanel[] result = new StackPanel[rank.Count + 1];

            result[0] = new StackPanel { Orientation = Orientation.Horizontal, FlowDirection = FlowDirection.RightToLeft };

            //#    name   score
            result[0].Children.Add(new TextBlock { Text = "#", Width = 36 });
            result[0].Children.Add(new TextBlock { Text = "تیم", Width = 150 });

            result[0].Children.Add(new TextBlock { Text = "امتیاز", Width = 48 });
            result[0].Children.Add(new TextBlock { Text = "ب", Width = 36 });
            result[0].Children.Add(new TextBlock { Text = "ت", Width = 36 });
            result[0].Children.Add(new TextBlock { Text = "ش", Width = 36 });

            switch (sport)
            {
                case "پرش با ویلچر"://fault
                    result[0].Children.Add(new TextBlock { Text = "خطا", Width = 36 });
                    break;
                case "فوتبال دستی نابینایان":
                    result[0].Children.Add(new TextBlock { Text = "زده", Width = 48 });
                    result[0].Children.Add(new TextBlock { Text = "خورده", Width = 48 });
                    result[0].Children.Add(new TextBlock { Text = "تفاضل", Width = 48 });
                    break;
                case "کانتر شبکه":
                    result[0].Children.Add(new TextBlock { Text = "کشتن", Width = 48 });
                    result[0].Children.Add(new TextBlock { Text = "تلفات", Width = 48 });
                    result[0].Children.Add(new TextBlock { Text = "تفاضل", Width = 48 });
                    break;
                default:
                    break;
            }

            for (int i = 1; i <= rank.Count; i++)
            {
                string teamname = rank.GetStringAt(Convert.ToUInt32(i - 1));
                JsonObject team = j["team"].GetObject()[teamname].GetObject();
                result[i] = new StackPanel { Orientation = Orientation.Horizontal, FlowDirection=FlowDirection.RightToLeft };

                //#    name   score
                result[i].Children.Add(new TextBlock { Text = i.ToString(), Width = 36}); 
                result[i].Children.Add(new TextBlock { Text = Useful.En_Fa(teamname), Width = 150 });

                result[i].Children.Add(new TextBlock { Text = (team["win"].GetNumber() * 3 + team["equal"].GetNumber()).ToString(), Width = 48 });
                result[i].Children.Add(new TextBlock { Text = team["win"].GetNumber().ToString(), Width = 36 });
                result[i].Children.Add(new TextBlock { Text = team["equal"].GetNumber().ToString(), Width = 36 });
                result[i].Children.Add(new TextBlock { Text = team["lost"].GetNumber().ToString(), Width = 36 });

                switch (sport)
                {
                    case "پرش با ویلچر"://fault
                        result[i].Children.Add(new TextBlock { Text = team["fault"].GetNumber().ToString(), Width = 36});
                        break;
                    case "فوتبال دستی نابینایان":
                        result[i].Children.Add(new TextBlock { Text = team["wg"].GetNumber().ToString(), Width = 48 });
                        result[i].Children.Add(new TextBlock { Text = team["lg"].GetNumber().ToString(), Width = 48 });
                        result[i].Children.Add(new TextBlock { Text = (team["wg"].GetNumber() - team["lg"].GetNumber()).ToString(), Width = 48 });
                        break;
                    case "کانتر شبکه":
                        result[i].Children.Add(new TextBlock { Text = team["wk"].GetNumber().ToString(), Width = 48 });
                        result[i].Children.Add(new TextBlock { Text = team["lk"].GetNumber().ToString(), Width = 48 });
                        result[i].Children.Add(new TextBlock { Text = (team["wk"].GetNumber() - team["lk"].GetNumber()).ToString(), Width = 48 });
                        break;
                    default:
                        break;
                }
            }
            return result;
        }
        public async Task<JsonObject> leagueinfo(string sport, string league)
        {
            JsonObject j;
            JsonObject.TryParse(await server.get($"type=leaguerank&sport={Useful.Fa_En(sport)}&league={Useful.Fa_En(league)}"), out j);
            return j;
        }
        public async Task<bool> addcompetition(string sport, string league, string team1, string team2, DateTimeOffset date, TimeSpan time, string place)
        {
            return await server.get($"type=competitionadd&sport={Useful.Fa_En(sport)}&league={Useful.Fa_En(league)}&competition={Useful.Fa_En(team1)}-{Useful.Fa_En(team2)}&info=$date$:{"{"}$y$:{date.Year},$m$:{date.Month},$d$:{date.Day}{"}"},$time$:{"{"}$h$:{time.Hours},$m$:{time.Minutes}{"}"},$place$:${Useful.Fa_En(place)}$") == "1";
        }
        public async Task<bool> editcompetition(string sport, string league, string team1, string team2, DateTimeOffset date, TimeSpan time, string place)
        {
            return await server.get($"type=competitionedit&sport={Useful.Fa_En(sport)}&league={Useful.Fa_En(league)}&competition={Useful.Fa_En(team1)}-{Useful.Fa_En(team2)}&newinfo=$date$:{"{"}$y$:{date.Year},$m$:{date.Month},$d$:{date.Day}{"}"},$time$:{"{"}$h$:{time.Hours},$m$:{time.Minutes}{"}"},$place$:${Useful.Fa_En(place)}$") == "1";
        }
        public async Task<bool> deletecompetition(string sport, string league, string team1, string team2)
        {
            return await server.get($"type=competitiondelete&sport={Useful.Fa_En(sport)}&league={Useful.Fa_En(league)}&competition={Useful.Fa_En(team1)}-{Useful.Fa_En(team2)}") == "1";
        }
        public async Task<bool> editresultcompetition(string sport, string league, string team1, string team2, string result, string info)
        {
            return await server.get($"type=competitionresult&sport={Useful.Fa_En(sport)}&league={Useful.Fa_En(league)}&competition={Useful.Fa_En(team1)}-{Useful.Fa_En(team2)}&result={result}&info={info}")=="1";
        }
        //unresulted past competitions
        public async Task<JsonObject> upc(string sport)
        {
            JsonObject res;
            JsonObject.TryParse(await server.get($"type=sportupc&sport={Useful.Fa_En(sport)}"), out res);
            return res;
        }
    }
}
