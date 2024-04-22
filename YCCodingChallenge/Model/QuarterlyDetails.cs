using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCCodingChallenge.Model
{
    public class QuarterlyDetails
    {
        public string EmployeeCode { get; set; } = "";
        public string Quarter { get; set; } = "";
        public decimal TotalOTE { get; set; }
        public decimal TotalSuperPayable { get; set; }
        public decimal TotalDisbursed { get; set; }
        public decimal Variance { get { return TotalSuperPayable - TotalDisbursed; } }
    }
}
