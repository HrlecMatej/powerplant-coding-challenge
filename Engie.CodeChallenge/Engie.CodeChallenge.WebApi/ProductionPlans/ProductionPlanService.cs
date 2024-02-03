using Engie.CodeChallenge.WebApi.ProductionPlans.Models;

namespace Engie.CodeChallenge.WebApi.ProductionPlans;

public class ProductionPlanService : IProductionPlanService
{
    public IEnumerable<ProductionPlanResult> GetProductionPlan(ProductionPlanRequest request)
    {
        // Sort power plants by cost
        var sortedPowerPlants = request.PowerPlants.OrderBy(p => CalculateCost(p, request.Fuels)).ToArray();

        // Check if we can meet the load
        if (sortedPowerPlants.Sum(p => p.Pmax) < request.Load)
        {
            // Perhaps do something, when there is insufficient power capacity...
            Console.WriteLine("Insufficient power capacity to meet the load.");
        }

        var productionPlanResults = new List<ProductionPlanResult>();
        var remainingLoad = request.Load;

        // Assign load to power plants
        foreach (var powerPlant in sortedPowerPlants)
        {
            if (remainingLoad == 0)
            {
                // The load has already been filled, so just add the powerPlant to the results and continue to the next one
                productionPlanResults.Add(new ProductionPlanResult { Name = powerPlant.Name, P = 0 });
                continue;
            }

            // Calculate if the remainingLoad exceeds powerPlant's Pmin
            var requiredReduction = powerPlant.Pmin - remainingLoad;
            if (requiredReduction > 0)
            {
                // We need to reduce the power output of the previous power plant
                var lastPlan = productionPlanResults.Last();
                lastPlan.P -= requiredReduction;

                // The remaining load now equals the current powerPlant's Pmin
                remainingLoad = powerPlant.Pmin;
            }

            // Calculate available power for the current power plant
            var availablePower = Math.Min(powerPlant.Pmax, remainingLoad);

            var producedPower = Math.Min(availablePower, remainingLoad);
            productionPlanResults.Add(new ProductionPlanResult { Name = powerPlant.Name, P = producedPower });

            remainingLoad -= producedPower;
        }

        return productionPlanResults;
    }

    private static double CalculateCost(PowerPlant powerPlant, FuelData fuels) =>
        powerPlant.Type switch
        {
            PowerPlantType.windturbine => 0,
            PowerPlantType.gasfired => fuels.GasPrice * (1 / powerPlant.Efficiency) + fuels.CO2Price * 0.3, // 0.3 ton of CO2 per MWh
            PowerPlantType.turbojet => fuels.KerosinePrice * (1 / powerPlant.Efficiency),
            _ => throw new ArgumentOutOfRangeException()
        };
}
