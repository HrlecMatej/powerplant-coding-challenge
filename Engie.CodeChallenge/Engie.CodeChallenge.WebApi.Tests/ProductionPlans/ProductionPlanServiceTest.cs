using System.Collections.Generic;
using Engie.CodeChallenge.WebApi.ProductionPlans;
using Engie.CodeChallenge.WebApi.ProductionPlans.Models;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace Engie.CodeChallenge.WebApi.Tests.ProductionPlans;

[TestSubject(typeof(ProductionPlanService))]
public class ProductionPlanServiceTest(
    [NotNull] ITestOutputHelper testOutputHelper,
    [NotNull] ProductionPlanFixture fixture
) : TestBed<ProductionPlanFixture>(testOutputHelper, fixture)
{
    private readonly IProductionPlanService _productionPlanService = fixture.GetService<IProductionPlanService>(
        testOutputHelper
    );

    [Theory]
    [MemberData(nameof(Payload3))]
    public void CalculateProductionPlanAndCheckResult(
        ProductionPlanRequest request,
        IEnumerable<ProductionPlanResult> result
    )
    {
        var actualResult = _productionPlanService.GetProductionPlan(request);

        actualResult.Should().BeEquivalentTo(result);
    }

    public static TheoryData<ProductionPlanRequest, IEnumerable<ProductionPlanResult>> Payload3 =>
        new()
        {
            // payload3.json
            {
                new ProductionPlanRequest
                {
                    Load = 910,
                    Fuels = new FuelData
                    {
                        GasPrice = 13.4,
                        KerosinePrice = 50.8,
                        CO2Price = 20,
                        WindPercentage = 60
                    },
                    PowerPlants = new PowerPlant[]
                    {
                        new()
                        {
                            Name = "gasfiredbig1",
                            Type = PowerPlantType.gasfired,
                            Efficiency = 0.53,
                            Pmin = 100,
                            Pmax = 460
                        },
                        new()
                        {
                            Name = "gasfiredbig2",
                            Type = PowerPlantType.gasfired,
                            Efficiency = 0.53,
                            Pmin = 100,
                            Pmax = 460
                        },
                        new()
                        {
                            Name = "gasfiredsomewhatsmaller",
                            Type = PowerPlantType.gasfired,
                            Efficiency = 0.37,
                            Pmin = 40,
                            Pmax = 210
                        },
                        new()
                        {
                            Name = "tj1",
                            Type = PowerPlantType.turbojet,
                            Efficiency = 0.53,
                            Pmin = 0,
                            Pmax = 16
                        },
                        new()
                        {
                            Name = "windpark1",
                            Type = PowerPlantType.windturbine,
                            Efficiency = 1,
                            Pmin = 0,
                            Pmax = 150
                        },
                        new()
                        {
                            Name = "windpark2",
                            Type = PowerPlantType.windturbine,
                            Efficiency = 1,
                            Pmin = 0,
                            Pmax = 36
                        },
                    }
                },
                new ProductionPlanResult[]
                {
                    new() { Name = "windpark1", P = 150 },
                    new() { Name = "windpark2", P = 36 },
                    new() { Name = "gasfiredbig1", P = 460 },
                    new() { Name = "gasfiredbig2", P = 264 },
                    new() { Name = "gasfiredsomewhatsmaller", P = 0 },
                    new() { Name = "tj1", P = 0 }
                }
            },
            // payload3.json, but with reduced Load, so that gasfiredbig1 has to reduce its output so that gasfiredbig2 can run with minimum output
            {
                new ProductionPlanRequest
                {
                    Load = 700,
                    Fuels = new FuelData
                    {
                        GasPrice = 13.4,
                        KerosinePrice = 50.8,
                        CO2Price = 20,
                        WindPercentage = 60
                    },
                    PowerPlants = new PowerPlant[]
                    {
                        new()
                        {
                            Name = "gasfiredbig1",
                            Type = PowerPlantType.gasfired,
                            Efficiency = 0.53,
                            Pmin = 100,
                            Pmax = 460
                        },
                        new()
                        {
                            Name = "gasfiredbig2",
                            Type = PowerPlantType.gasfired,
                            Efficiency = 0.53,
                            Pmin = 100,
                            Pmax = 460
                        },
                        new()
                        {
                            Name = "gasfiredsomewhatsmaller",
                            Type = PowerPlantType.gasfired,
                            Efficiency = 0.37,
                            Pmin = 40,
                            Pmax = 210
                        },
                        new()
                        {
                            Name = "tj1",
                            Type = PowerPlantType.turbojet,
                            Efficiency = 0.53,
                            Pmin = 0,
                            Pmax = 16
                        },
                        new()
                        {
                            Name = "windpark1",
                            Type = PowerPlantType.windturbine,
                            Efficiency = 1,
                            Pmin = 0,
                            Pmax = 150
                        },
                        new()
                        {
                            Name = "windpark2",
                            Type = PowerPlantType.windturbine,
                            Efficiency = 1,
                            Pmin = 0,
                            Pmax = 36
                        },
                    }
                },
                new ProductionPlanResult[]
                {
                    new() { Name = "windpark1", P = 150 },
                    new() { Name = "windpark2", P = 36 },
                    new() { Name = "gasfiredbig1", P = 414 },
                    new() { Name = "gasfiredbig2", P = 100 },
                    new() { Name = "gasfiredsomewhatsmaller", P = 0 },
                    new() { Name = "tj1", P = 0 }
                }
            }
        };
}
