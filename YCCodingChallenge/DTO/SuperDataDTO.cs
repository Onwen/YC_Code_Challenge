using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCCodingChallenge.DTO
{
    public class SuperDataDTO(List<DisbursementDTO> disbursements, List<PaySlipDTO> payslips, List<PayCodeDTO> paycodes)
    {
        public readonly List<DisbursementDTO> Disbursements = disbursements;
        public readonly List<PaySlipDTO> PaySlips = payslips;
        public readonly List<PayCodeDTO> PayCodes = paycodes;
    }
}
