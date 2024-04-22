using AutoFixture;
using Castle.Core.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCCodingChallenge.Adapter;
using YCCodingChallenge.Adapter.Interface;
using YCCodingChallenge.DTO;
using YCCodingChallenge.Model.Configuration;
using YCCodingChallenge.Repository;
using YCCodingChallenge.Repository.Interface;
using YCCodingChallenge.Service;
using YCCodingChallenge.Service.Interface;

namespace YCCodingChallenge.Tests.IntegrationTests
{
    public class SuperDisbursementServiceIntegrationTests
    {
        SuperDataConfiguration configuration;
        ISuperDataRepository repository;
        ISuperDataRepositoryAdapter adapter;
        ISuperDisbursementService superDisbursementService;

        [SetUp]
        public void Setup()
        {
            configuration = new SuperDataConfiguration("../../../TestData/Test Super Data.xlsx");
            repository = new SuperDataRepository(configuration);
            adapter = new SuperDataRepositoryAdapter(repository);
            superDisbursementService = new SuperDisbursementService(adapter);
        }

        [Test]
        public void Successfully_Calculates()
        {
            configuration = new SuperDataConfiguration("../../../TestData/Test Super Data.xlsx");
            repository = new SuperDataRepository(configuration);
            adapter = new SuperDataRepositoryAdapter(repository);
            superDisbursementService = new SuperDisbursementService(adapter);

            var result = superDisbursementService.CalculateSuperDisbursements();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].TotalOTE, Is.EqualTo(15000));
            Assert.That(result[0].TotalSuperPayable, Is.EqualTo(1425));
            Assert.That(result[0].TotalDisbursed, Is.EqualTo(1000));
            Assert.That(result[0].Variance, Is.EqualTo(425));
        }
    }
}
