using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testcsh
{
    abstract class account
    {
        protected string name, username, password, email, phone, code;
        protected bool isman;

        public abstract bool signin(string user, string pass);
        public virtual bool checkesignup() { return true; }
        public virtual bool signup() { return true; }

    }
}
