using AutoFixture;
using Moq;
using YCCodingChallenge.Adapter;
using YCCodingChallenge.DTO;
using YCCodingChallenge.Repository.Interface;

namespace YCCodingChallenge.Tests.UnitTests
{
    public class SuperDataRepositoryAdapterTests
    {
        Fixture _fixture;
        Mock<ISuperDataRepository> _mockSuperDataRespository;
        SuperDataRepositoryAdapter _superDataRepositoryAdapter;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockSuperDataRespository = new Mock<ISuperDataRepository>();
            _superDataRepositoryAdapter = new SuperDataRepositoryAdapter(_mockSuperDataRespository.Object);
        }

        [Test]
        public void GetEmployeeData_Successfully_Maps_Data()
        {
            var disbursements = _fixture.Build<DisbursementDTO>().With(b => b.EmployeeCode, "employee").CreateMany().ToList();
            var payslips = _fixture.Build<PaySlipDTO>().With(b => b.EmployeeCode, "employee").CreateMany().ToList();
            var superData = _fixture.Build<SuperDataDTO>().With(b => b.Disbursements, disbursements).With(b => b.PaySlips, payslips).Create();
            _mockSuperDataRespository.Setup(b => b.GetSuperData()).Returns(superData);
            var result = _superDataRepositoryAdapter.GetEmployeeData();

            Assert.NotNull(result);
            Assert.That(result.Employees.Count, Is.EqualTo(1));
            Assert.That(result.Employees[0].EmployeeCode, Is.EqualTo("employee"));
            Assert.That(result.Employees[0].Disbursements.Count, Is.EqualTo(disbursements.Count()));
            Assert.That(result.Employees[0].Disbursements[0].SuperGuaranteeAmount, Is.EqualTo(disbursements[0].SGCAmount));
            Assert.That(result.Employees[0].Disbursements[0].PaymentMadeDate, Is.EqualTo(disbursements[0].PaymentMade));
            Assert.That(result.Employees[0].Disbursements[0].PayPeriodFrom, Is.EqualTo(disbursements[0].PayPeriodFrom));
            Assert.That(result.Employees[0].Disbursements[0].PayPeriodTo, Is.EqualTo(disbursements[0].PayPeriodTo));
            Assert.That(result.Employees[0].PaySlips.Count, Is.EqualTo(payslips.Count()));
            Assert.That(result.Employees[0].PaySlips[0].Amount, Is.EqualTo(payslips[0].Amount));
            Assert.That(result.Employees[0].PaySlips[0].PaySlipId, Is.EqualTo(payslips[0].PaySlipId));
            Assert.That(result.Employees[0].PaySlips[0].PayCode, Is.EqualTo(payslips[0].PayCode));
            Assert.That(result.Employees[0].PaySlips[0].End, Is.EqualTo(payslips[0].EndDate));
            Assert.That(result.Paycodes.Count, Is.EqualTo(superData.PayCodes.Count));
        }
    }
}
