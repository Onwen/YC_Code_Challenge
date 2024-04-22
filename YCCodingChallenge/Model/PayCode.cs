using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCCodingChallenge.Model
{
    public class PayCode
    {
        private const string OTE = "OTE";
        public string OTETreatment = "";

        public string Code = "";
        public bool IsOTE { get { return OTETreatment == OTE; } }
    }
}
