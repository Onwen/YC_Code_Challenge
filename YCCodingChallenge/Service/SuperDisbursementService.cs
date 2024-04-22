using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCCodingChallenge.Adapter.Interface;
using YCCodingChallenge.Model;
using YCCodingChallenge.Model.Extension;
using YCCodingChallenge.Service.Interface;

namespace YCCodingChallenge.Service
{
    public class SuperDisbursementService(ISuperDataRepositoryAdapter superDataRepositoryAdapter) : ISuperDisbursementService
    {
        public List<QuarterlyDetails> CalculateSuperDisbursements()
        {
            var employeeData = superDataRepositoryAdapter.GetEmployeeData();
            List<QuarterlyDetails> details = [];

            foreach(var e in employeeData.Employees)
            {
                var employeeDetails = GetQuarterlyDetailsForEmployee(e, employeeData.Paycodes);
                details.AddRange(employeeDetails);
            }

            return details;
        }

        private List<QuarterlyDetails> GetQuarterlyDetailsForEmployee(Employee employee, Dictionary<string, PayCode> paycodes) {
            var payslips = employee.PaySlips.OrderBy(b => b.End).ToList();
            var disbursements = employee.Disbursements.OrderBy(b => b.PaymentMadeDate).ToList();

            Dictionary<string, QuarterlyDetails> details = [];
            foreach (var payslip in payslips)
            {
                // Get String identifying quarter (eg 2024-Q1)
                var quarter = $"{payslip.End.Year}-Q{payslip.End.Quarter()}";

                var oteAmt = paycodes.IsOTE(payslip.PayCode) ? payslip.Amount : 0;
                var superPayable = decimal.Round(oteAmt * 0.095m,2,MidpointRounding.AwayFromZero);
                if (details.ContainsKey(quarter))
                {
                    details[quarter].TotalOTE += oteAmt;
                    details[quarter].TotalSuperPayable += superPayable;
                    continue;
                }
                details.Add(quarter, new QuarterlyDetails()
                {
                    EmployeeCode = employee.EmployeeCode,
                    Quarter = quarter,
                    TotalOTE = oteAmt,
                    TotalSuperPayable = superPayable
                });
            }
            foreach (var disbursement in disbursements)
            {
                var backdate = disbursement.PaymentMadeDate.AddDays(-28);
                // Get String identifying quarter (eg 2024-Q1)
                var quarter = $"{backdate.Year}-Q{backdate.Quarter()}";

                var amount = disbursement.SuperGuaranteeAmount;
                if (details.ContainsKey(quarter))
                {
                    details[quarter].TotalDisbursed += amount;
                    continue;
                }
                details.Add(quarter, new QuarterlyDetails()
                {
                    EmployeeCode = employee.EmployeeCode,
                    Quarter = quarter,
                    TotalDisbursed = amount
                });
            }

            return details.Values.ToList();
        }
    }
}
