using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCCodingChallenge.Model
{
    public class PaySlip
    {
        public Guid PaySlipId;
        public DateTime End;
        public string PayCode = "";
        public decimal Amount;
    }
}
