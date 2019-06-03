using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testcsh
{
    abstract class Account
    {
        protected string name, username, password, email, phone, code;
        protected bool isman;

        public abstract bool signin(string user, string pass);
        //is username exist?
        static public async Task<bool> Checkusername(string user)
        {
            server s = new server("1379", $"type=userfind&user={user}");
            string res = await s.get();

            return (res == "1");
        }
        static public async Task<bool> Activeaccount(string user, string pass, string code)
        {
            server s = new server("1379", $"type=useractive&user={user}&pass={pass}&code={code}");
            string res = await s.get();

            return (res == "1");
        }
        static public async Task<bool> Generatecode(string user, string pass)
        {
            server s = new server("1379", $"type=usergenerate&user={user}&pass={pass}");
            string res = await s.get();

            return (res == "1");
        }
        static public async Task<bool> Retrievepass(string user, string Email)
        {
            server s = new server("1379", $"type=userretrieve&user={user}&email={Email}");
            string res = await s.get();

            return (res == "1");
        }
    }
}
