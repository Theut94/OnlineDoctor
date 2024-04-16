using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModels
{
    public class Patient
    {
        public string ssn { get; set; }
        public string mail { get; set; }
        public string name { get; set; }
        public Measurement[] measurements { get; set; }
    }
}
