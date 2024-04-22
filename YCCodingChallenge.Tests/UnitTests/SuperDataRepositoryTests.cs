using YCCodingChallenge.Model.Configuration;
using YCCodingChallenge.Repository;

namespace YCCodingChallenge.Tests.UnitTests
{
    public class SuperDataRepositoryTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetSuperData_Valid_File_Reads_Successfully()
        {
            var superDataConfiguration = new SuperDataConfiguration("../../../TestData/Sample Super Data.xlsx");
            var repository = new SuperDataRepository(superDataConfiguration);
            var result = repository.GetSuperData();

            Assert.That(result.Disbursements.Count, Is.EqualTo(75));
            Assert.That(result.PaySlips.Count, Is.EqualTo(614));
            Assert.That(result.PayCodes.Count, Is.EqualTo(26));
        }

        [Test]
        public void GetSuperData_Invalid_Data_Returns_Empty_Result()
        {
            var superDataConfiguration = new SuperDataConfiguration("../../../TestData/Invalid Sample Super Data.xlsx");
            var repository = new SuperDataRepository(superDataConfiguration);
            var result = repository.GetSuperData();

            Assert.IsNotNull(result);
            Assert.IsEmpty(result.Disbursements);
            Assert.IsEmpty(result.PaySlips);
            Assert.IsNotEmpty(result.PayCodes);
        }

        [Test]
        public void GetSuperData_Non_Existant_File_Returns_Throws_Not_Found_Exception()
        {
            var superDataConfiguration = new SuperDataConfiguration("../../../TestData/Doesnt Exist.xlsx");
            var repository = new SuperDataRepository(superDataConfiguration);
            Assert.Throws(typeof(FileNotFoundException), () => repository.GetSuperData());
        }

        [Test]
        public void GetSuperData_Empty_Filepath_Throws_Argument_Exception()
        {
            var superDataConfiguration = new SuperDataConfiguration("");
            var repository = new SuperDataRepository(superDataConfiguration);
            Assert.Throws(typeof(ArgumentException), () => repository.GetSuperData());
        }
    }
}