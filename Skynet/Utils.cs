using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skynet
{
    class Utils
    {
        public static string ConnString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
    }
}
