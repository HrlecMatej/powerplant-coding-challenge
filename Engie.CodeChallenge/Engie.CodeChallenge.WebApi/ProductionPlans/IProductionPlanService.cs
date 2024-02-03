using Engie.CodeChallenge.WebApi.ProductionPlans.Models;

namespace Engie.CodeChallenge.WebApi.ProductionPlans;

public interface IProductionPlanService
{
    /// <summary>
    /// Calculates a production plan for the request load.
    /// </summary>
    /// <param name="request">Required load, prices for fuels and available power plants.</param>
    /// <returns>Collection of power plants and their loads.</returns>
    IEnumerable<ProductionPlanResult> GetProductionPlan(ProductionPlanRequest request);
}
