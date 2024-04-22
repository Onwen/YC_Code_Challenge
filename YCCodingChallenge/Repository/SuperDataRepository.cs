using ExcelDataReader;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using YCCodingChallenge.DTO;
using YCCodingChallenge.Model;
using YCCodingChallenge.Model.Configuration;
using YCCodingChallenge.Repository.Interface;

namespace YCCodingChallenge.Repository
{
    public class SuperDataRepository(SuperDataConfiguration configuration) : ISuperDataRepository
    {
        public SuperDataDTO GetSuperData()
        {
            SuperDataDTO data; 
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(configuration.filepath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    var disbursements = GetDisbursements(result.Tables["Disbursements"]);
                    var paySlips = GetPayslips(result.Tables["PaySlips"]);
                    var payCodes = GetPayCodes(result.Tables["PayCodes"]);

                    data = new(disbursements, paySlips, payCodes);  
                }
            }
            return data;
        }

        private List<DisbursementDTO> GetDisbursements(DataTable dataTable)
        {
            List<DisbursementDTO> result = [];

            if (dataTable == null) { return []; }

            try
            {
                var first = true;
                foreach (DataRow row in dataTable.Rows)
                {
                    if (first)
                    {
                        first = false;
                        continue;
                    }
                    string sgc_amount = row[0]?.ToString() ?? throw new NullReferenceException();
                    string payment_made = row[1]?.ToString() ?? throw new NullReferenceException();
                    string pay_period_from = row[2]?.ToString() ?? throw new NullReferenceException();
                    string pay_period_to = row[3]?.ToString() ?? throw new NullReferenceException();
                    string employee_code = row[4]?.ToString() ?? throw new NullReferenceException();
                    result.Add(new DisbursementDTO() { 
                        SGCAmount = decimal.TryParse(sgc_amount, out decimal amount) ? amount : throw new ArgumentException("Unparsable amount", nameof(sgc_amount)),
                        PaymentMade = DateTime.TryParse(payment_made, out DateTime made) ? made : throw new ArgumentException("Unparsable date", nameof(payment_made)), 
                        PayPeriodFrom = DateTime.TryParse(pay_period_from, out DateTime from) ? from : throw new ArgumentException("Unparsable date", nameof(pay_period_from)), 
                        PayPeriodTo = DateTime.TryParse(pay_period_to, out DateTime to) ? to : throw new ArgumentException("Unparsable date", nameof(pay_period_to)), 
                        EmployeeCode = employee_code });
                }
            }
            catch (Exception ex)
            {
                return [];
            }
            
            return result;
        }

        private List<PaySlipDTO> GetPayslips(DataTable dataTable)
        {
            List<PaySlipDTO> result = [];

            if (dataTable == null) { return []; }

            try
            {
                var first = true;
                foreach (DataRow row in dataTable.Rows)
                {
                    if (first)
                    {
                        first = false;
                        continue;
                    }
                    string payslip_id = row[0]?.ToString() ?? throw new NullReferenceException();
                    string end = row[1]?.ToString() ?? throw new NullReferenceException();
                    string employee_code = row[2]?.ToString() ?? throw new NullReferenceException();
                    string code = row[3]?.ToString() ?? throw new NullReferenceException();
                    string amount = row[4]?.ToString() ?? throw new NullReferenceException();
                    result.Add(new PaySlipDTO() { 
                        PaySlipId = Guid.TryParse(payslip_id, out Guid id) ? id : throw new ArgumentException("Unparsable guid", nameof(payslip_id)),
                        EndDate = DateTime.TryParse(end, out DateTime endDate) ? endDate : throw new ArgumentException("Unparsable date", nameof(end)),
                        EmployeeCode = employee_code,
                        PayCode = code,
                        Amount = decimal.TryParse(amount, out decimal amt) ? amt : throw new ArgumentException("Unparsable amount", nameof(amount))
                    });
                }
            }
            catch (Exception ex)
            {
                return [];
            }

            return result;
        }

        private List<PayCodeDTO> GetPayCodes(DataTable dataTable)
        {
            List<PayCodeDTO> result = [];

            if (dataTable == null) { return []; }

            try
            {
                var first = true;
                foreach (DataRow row in dataTable.Rows)
                {
                    if (first)
                    {
                        first = false;
                        continue;
                    }
                    string pay_code = row[0]?.ToString() ?? throw new NullReferenceException();
                    string ote_treament = row[1]?.ToString() ?? throw new NullReferenceException();
                    result.Add(new PayCodeDTO() { PayCode = pay_code, Ote = ote_treament});
                }
            }
            catch (Exception ex)
            {
                return [];
            }

            return result;
        }
    }
}
