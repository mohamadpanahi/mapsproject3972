using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testui
{
    enum AdminAccessabilty { PareshBaVilcher, FootbalDastiNabinayan, CounterShabake, Administrator }
    class Admin : Account
    {
        public AdminAccessabilty accessabilty { get; set; }
    }
}