using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCCodingChallenge.Model
{
    public class Employee(string employee_code, List<Disbursement> disbursements, List<PaySlip> pay_slips)
    {
        public readonly string EmployeeCode = employee_code;
        public List<Disbursement> Disbursements = disbursements;
        public List<PaySlip> PaySlips = pay_slips;
    }
}
