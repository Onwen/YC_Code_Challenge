// See https://aka.ms/new-console-template for more information
using YCCodingChallenge.Adapter;
using YCCodingChallenge.Model.Configuration;
using YCCodingChallenge.Repository;
using YCCodingChallenge.Service;

var configuration = new SuperDataConfiguration("../../../TestData/Sample Super Data.xlsx");
var repository = new SuperDataRepository(configuration);
var adapter = new SuperDataRepositoryAdapter(repository);
var superDisbursementService = new SuperDisbursementService(adapter);

var details = superDisbursementService.CalculateSuperDisbursements().OrderBy(b => $"{b.EmployeeCode}-{b.Quarter}");

foreach(var detailsItem in details)
{
    Console.WriteLine($"{detailsItem.Quarter} - {detailsItem.EmployeeCode}");
    Console.WriteLine($"    Total OTE = ${detailsItem.TotalOTE}");
    Console.WriteLine($"    Total Super Payable = ${detailsItem.TotalSuperPayable}");
    Console.WriteLine($"    Total Disbursed = ${detailsItem.TotalDisbursed}");
    Console.WriteLine($"    Variance = ${detailsItem.Variance}");
}

Console.ReadLine();
