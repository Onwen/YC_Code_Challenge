using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCCodingChallenge.DTO
{
    public class PaySlipDTO
    {
        public Guid PaySlipId { get; set; }
        public DateTime EndDate { get; set; }
        public string EmployeeCode { get; set; } = "";
        public string PayCode { get; set; } = "";
        public decimal Amount { get; set; }
    }
}
