using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCCodingChallenge.DTO
{
    public class DisbursementDTO
    {
        public decimal SGCAmount { get; set; }
        public DateTime PaymentMade { get; set; }
        public DateTime PayPeriodFrom { get; set; }
        public DateTime PayPeriodTo { get; set; }
        public string EmployeeCode { get; set; } = "";
    }
}
