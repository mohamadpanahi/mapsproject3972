using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testui
{
    enum PlayerSport { PareshBaVilcher, FootbalDastiNabinayan, CounterShabake }
    //del | edit |signup team | del team |
    class Player : Account
    {
        public string stature, weight;
        public DateTimeOffset birth;
        public PlayerSport sport;
        private Server server = new Server();

        public async Task<bool> edit(string user, string pass, string newname, string newemail, string newcode, string newphone, bool newisman, string newpass, string newstature, string newweight, DateTimeOffset newbirthday)
        {
            return await server.get($"type=useredit&user={user}&pass={pass}&newinfo=$name$:${Useful.Fa_En(newname)}$,$email$:${newemail}$,$code$:${newcode}$,$phone$:${newphone}$,$isman$:{(newisman ? "true" : "false")},$pass$:${newpass}$,$stature$:${newstature}$,$weight$:${newweight}$,$birthday$:{"{"}$y$:{newbirthday.Year},$m$:{newbirthday.Month},$d$:{newbirthday.Day}{"}"}") == "1";
        }
        public async Task<bool> addteam(string league, string team, params string[] member)
        {
            string sport = "";
            switch (this.sport)
            {
                case PlayerSport.PareshBaVilcher:
                    sport = "clp ba DFAgl";
                    break;
                case PlayerSport.FootbalDastiNabinayan:
                    sport = "wDdbaA jodF CabFCaFaC";
                    break;
                case PlayerSport.CounterShabake:
                    sport = "yaCdl pbyE";
                    break;
            }

            string members = "";
            foreach (var m in member)
            {
                members += $"${Useful.Fa_En(m)}$:" + "{},";
            }
            members = members.Remove(members.Length - 1);
            return await server.get($"type=teamadd&sport={sport}&league={Useful.Fa_En(league)}&team={Useful.Fa_En(team)}&member={members}") == "1";
        }
    }
}
