using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCCodingChallenge.Adapter.Interface;
using YCCodingChallenge.Model;
using YCCodingChallenge.Repository.Interface;

namespace YCCodingChallenge.Adapter
{
    public class SuperDataRepositoryAdapter(ISuperDataRepository superDataRepository) : ISuperDataRepositoryAdapter
    {
        public EmployeeData GetEmployeeData()
        {
            Dictionary<string, Employee> employees = [];
            var data = superDataRepository.GetSuperData();

            foreach (var disbursement in data.Disbursements)
            {
                var d = new Disbursement() { 
                    SuperGuaranteeAmount = disbursement.SGCAmount, 
                    PaymentMadeDate = disbursement.PaymentMade, 
                    PayPeriodFrom = disbursement.PayPeriodFrom, 
                    PayPeriodTo = disbursement.PayPeriodTo 
                };
                if (employees.ContainsKey(disbursement.EmployeeCode))
                {
                    employees[disbursement.EmployeeCode].Disbursements.Add(d);
                    continue;
                }
                employees.Add(disbursement.EmployeeCode, new Employee(disbursement.EmployeeCode, [d], []));
            }

            foreach (var payslip in data.PaySlips)
            {
                var p = new PaySlip() { PaySlipId = payslip.PaySlipId, End = payslip.EndDate, PayCode = payslip.PayCode, Amount = payslip.Amount };
                if (employees.ContainsKey(payslip.EmployeeCode))
                {
                    employees[payslip.EmployeeCode].PaySlips.Add(p);
                    continue;
                }
                employees.Add(payslip.EmployeeCode, new Employee(payslip.EmployeeCode, [], [p]));
            }

            var paycodes = data.PayCodes.Select(p => new PayCode() { Code = p.PayCode, OTETreatment = p.Ote }).ToDictionary(b => b.Code);

            return new EmployeeData() { Employees = employees.Values.ToList(), Paycodes = paycodes };
        }
    }
}
