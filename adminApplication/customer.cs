using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adminApplication
{
    class customer
    {
        public string Name { get; set; }
        public string date { get; set; }
        public string subk1 { get; set; }

        public static bool IsEqual(customer cst1, customer cst2)
        {
            if (cst1 == null || cst2 == null) { return false; }
            if (cst1.subk1 == cst2.subk1) { return true; }
            return false;
        }
    }
}
