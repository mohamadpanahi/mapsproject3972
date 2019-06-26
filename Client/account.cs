using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace testui
{
    abstract class Account
    {
        private static Server server = new Server();
        public string Code { get; protected set; }
        public string Email { get; protected set; }
        public bool IsMan { get; protected set; }
        public string Name { get; protected set; }
        public string Password { get; protected set; }
        public string Phone { get; protected set; }
        public string Username { get; protected set; }

        public static async Task<int> Signin(string user, string pass)
        {
            string res = await server.get($"type=usersignin&user={user}&pass={pass}");
            return Convert.ToInt32(res);
        }
        public static async Task<Account> GetUserInfo(string user, string pass)
        {
            JsonObject info;
            JsonObject.TryParse(await server.get($"type=userinfo&user={user}&pass={pass}"), out info);
            Account result;

            try
            {
                if (info["isplayer"].GetBoolean())
                {
                    result = new User();
                }
                else
                    result = new Player();
            }
            catch
            {
                if (info["isplayer"].GetString() == "admin")
                {
                    Admin admin = new Admin();
                    switch (info["sport"].GetString())
                    {
                        case "administrator":
                            admin.accessabilty = AdminAccessabilty.Administrator;
                            break;
                        case "wDdbaA jodF CabFCaFaC":
                            admin.accessabilty = AdminAccessabilty.FootbalDastiNabinayan;
                            break;
                        case "clp ba DFAgl":
                            admin.accessabilty = AdminAccessabilty.PareshBaVilcher;
                            break;
                        default:
                            admin.accessabilty = AdminAccessabilty.CounterShabake;
                            break;
                    }

                    result = admin;
                }
                else
                    result = null;
            }
            if (result != null)
            {
                result.Username = user;
                result.Password = pass;
                result.Code = info["code"].GetString();
                result.Email = info["email"].GetString();
                result.IsMan = info["isman"].GetBoolean();
                result.Name = Useful.En_Fa(info["name"].GetString());
                result.Phone = info["phone"].GetString();
            }
            return result;
        }
        public static async Task<bool> Signup(string user, string pass, string email, string name, string phone, string code, bool isman)
        {
            string res = await server.get($"type=usersignup&user={user}&pass={pass}&email={email}&info=$name$:${Useful.Fa_En(name)}$,$phone$:${phone}$,$code$:${code}$,$isman$:{(isman ? "true" : "false")},$isplayer$:false");
            return res == "1";
        }
        public static async Task<bool> Signup(string user, string pass, string email, string name, string phone, string code, bool isman, DateTimeOffset birthday, string stature, string weight, string sport)
        {
            string res = await server.get($"type=usersignup&user={user}&pass={pass}&email={email}&info=$name$:${Useful.Fa_En(name)}$,$phone$:${phone}$,$code$:${code}$,$isman$:{(isman ? "true" : "false")},$isplayer$:true,$birthday$:${birthday.Date.Year} {birthday.Date.Month} {birthday.Date.Day}$,$stature$:${stature}$,$weight$:${weight}$,$sport$:${Useful.Fa_En(sport)}$");
            return res == "1";
        }
        //return true if username exist
        public static async Task<bool> Checkusername(string user)
        {
            string res = await server.get($"type=userfind&user={user}");
            return res == "1";
        }
        public static async Task<bool> Activeaccount(string user, string pass, string code)
        {
            string res = await server.get($"type=useractive&user={user}&pass={pass}&code={code}");
            return res == "1";
        }
        public static async Task<bool> Generatecode(string user, string pass)
        {
            string res = await server.get($"type=usergenerate&user={user}&pass={pass}");
            return res == "1";
        }
        public static async Task<bool> Retrievepass(string user, string Email)
        {
            string res = await server.get($"type=userretrieve&user={user}&email={Email}");
            return res == "1";
        }
    }
}
