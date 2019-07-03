using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testui
{
    //del | edit |add fav | del fav | rate 
    class User : Account
    {
        private Server server = new Server();
        public async Task<bool> edit(string user, string pass, string newname, string newemail, string newcode, string newphone, bool newisman, string newpass)
        {
            return await server.get($"type=useredit&user={user}&pass={pass}&newinfo=$name$:${Useful.Fa_En(newname)}$,$email$:${newemail}$,$code$:${newcode}$,$phone$:${newphone}$,$isman$:{(newisman ? "true" : "false")},$pass$:${newpass}$") == "1";
        }
    }
}
