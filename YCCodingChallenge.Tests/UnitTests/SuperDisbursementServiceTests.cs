using AutoFixture;
using Moq;
using YCCodingChallenge.Adapter.Interface;
using YCCodingChallenge.Model;
using YCCodingChallenge.Service;
using YCCodingChallenge.Service.Interface;

namespace YCCodingChallenge.Tests.UnitTests
{
    public class SuperDisbursementServiceTests
    {
        Fixture _fixture;
        Mock<ISuperDataRepositoryAdapter> _mockSuperDataRespositoryAdapter;
        ISuperDisbursementService _service;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockSuperDataRespositoryAdapter = new Mock<ISuperDataRepositoryAdapter>();
            _service = new SuperDisbursementService(_mockSuperDataRespositoryAdapter.Object);
        }

        [Test]
        public void CalculateSuperDisbursements_Random_Data_Success()
        {
            var employeeData = _fixture.Build<EmployeeData>().Create();
            _mockSuperDataRespositoryAdapter.Setup(b => b.GetEmployeeData()).Returns(employeeData);
            var result = _service.CalculateSuperDisbursements();

            Assert.NotNull(result);
        }

        [Test]
        public void CalculateSuperDisbursements_No_Payslips_Success()
        {
            var employees = _fixture.Build<Employee>().With(b => b.PaySlips, []).CreateMany().ToList();
            var employeeData = _fixture.Build<EmployeeData>().With(b => b.Employees, employees).Create();
            _mockSuperDataRespositoryAdapter.Setup(b => b.GetEmployeeData()).Returns(employeeData);
            var result = _service.CalculateSuperDisbursements();

            Assert.NotNull(result);
            Assert.That(result.Count, Is.AtLeast(1));
            Assert.That(result[0].Variance, Is.EqualTo(-result[0].TotalDisbursed));
        }

        [Test]
        public void CalculateSuperDisbursements_No_Disbursements_Success()
        {
            var employees = _fixture.Build<Employee>().With(b => b.Disbursements, []).CreateMany().ToList();
            var employeeData = _fixture.Build<EmployeeData>().With(b => b.Employees, employees).Create();
            _mockSuperDataRespositoryAdapter.Setup(b => b.GetEmployeeData()).Returns(employeeData);
            var result = _service.CalculateSuperDisbursements();

            Assert.NotNull(result);
            Assert.That(result.Count, Is.AtLeast(1));
            Assert.That(result[0].Variance, Is.EqualTo(result[0].TotalSuperPayable));
        }

        [Test]
        public void CalculateSuperDisbursements_Test_Data_Success()
        {
            List<PayCode> paycodes = [
                new PayCode(){ Code = "Salary", OTETreatment = "OTE" },
                new PayCode(){ Code = "Site Allowance", OTETreatment = "OTE" },
                new PayCode(){ Code = "Overtime - Weekend", OTETreatment = "Not OTE" },
                new PayCode(){ Code = "Super Withheld", OTETreatment = "Not OTE" }
                ];
            List<PaySlip> payslips = [
                new PaySlip(){ PaySlipId = new Guid(), End = new DateTime(2024, 01, 1), PayCode = "Salary", Amount = 5000 },
                new PaySlip(){ PaySlipId = new Guid(), End = new DateTime(2024, 01, 1), PayCode = "Overtime - Weekend", Amount = 1500 },
                new PaySlip(){ PaySlipId = new Guid(), End = new DateTime(2024, 01, 1), PayCode = "Super Withheld", Amount = 475 },
                new PaySlip(){ PaySlipId = new Guid(), End = new DateTime(2024, 02, 1), PayCode = "Salary", Amount = 5000 },
                new PaySlip(){ PaySlipId = new Guid(), End = new DateTime(2024, 02, 1), PayCode = "Super Withheld", Amount = 475 },
                new PaySlip(){ PaySlipId = new Guid(), End = new DateTime(2024, 03, 1), PayCode = "Salary", Amount = 5000 },
                new PaySlip(){ PaySlipId = new Guid(), End = new DateTime(2024, 03, 1), PayCode = "Super Withheld", Amount = 475 },
                ];
            List<Disbursement> disbursements = [
                new Disbursement(){ SuperGuaranteeAmount = 500, PaymentMadeDate = new DateTime(2024, 2, 27), PayPeriodFrom = DateTime.Now, PayPeriodTo = DateTime.Now },
                new Disbursement(){ SuperGuaranteeAmount = 500, PaymentMadeDate = new DateTime(2024, 3, 30), PayPeriodFrom = DateTime.Now, PayPeriodTo = DateTime.Now },
                new Disbursement(){ SuperGuaranteeAmount = 500, PaymentMadeDate = new DateTime(2024, 4, 30), PayPeriodFrom = DateTime.Now, PayPeriodTo = DateTime.Now },
                ];
            var employee = new Employee("employee", disbursements, payslips);

            var employeeData = _fixture.Build<EmployeeData>().With(b => b.Employees, [employee]).With(b => b.Paycodes, paycodes.ToDictionary(b => b.Code)).Create();
            _mockSuperDataRespositoryAdapter.Setup(b => b.GetEmployeeData()).Returns(employeeData);
            var result = _service.CalculateSuperDisbursements();

            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].TotalOTE, Is.EqualTo(15000));
            Assert.That(result[0].TotalSuperPayable, Is.EqualTo(1425));
            Assert.That(result[0].TotalDisbursed, Is.EqualTo(1000));
            Assert.That(result[0].Variance, Is.EqualTo(425));
        }
    }
}
