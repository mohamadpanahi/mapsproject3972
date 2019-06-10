using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testui
{
    abstract class Account
    {
        protected string name, username, password, email, phone, code;
        protected bool isman;

        public static async Task<int> Signin(string user, string pass)
        {
            server s = new server("1379", $"type=usersignin&user={user}&pass={pass}");
            string res = await s.get();

            return Convert.ToInt32(res);
        }
        //return true if username exist
        public static async Task<bool> Checkusername(string user)
        {
            server s = new server("1379", $"type=userfind&user={user}");
            string res = await s.get();

            return (res == "1");
        }
        public static async Task<bool> Activeaccount(string user, string pass, string code)
        {
            server s = new server("1379", $"type=useractive&user={user}&pass={pass}&code={code}");
            string res = await s.get();

            return (res == "1");
        }
        public static async Task<bool> Generatecode(string user, string pass)
        {
            server s = new server("1379", $"type=usergenerate&user={user}&pass={pass}");
            string res = await s.get();

            return (res == "1");
        }
        public static async Task<bool> Retrievepass(string user, string Email)
        {
            server s = new server("1379", $"type=userretrieve&user={user}&email={Email}");
            string res = await s.get();

            return (res == "1");
        }
    }
}
