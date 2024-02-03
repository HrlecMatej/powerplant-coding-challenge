using Engie.CodeChallenge.WebApi.ProductionPlans.Models;
using Microsoft.AspNetCore.Mvc;

namespace Engie.CodeChallenge.WebApi.ProductionPlans;

[Route("api/[controller]")]
[ApiController]
public class ProductionPlanController(IProductionPlanService productionPlanService) : ControllerBase
{
    [HttpPost]
    public IEnumerable<ProductionPlanResult> GetProductionPlan([FromBody] ProductionPlanRequest request) =>
        productionPlanService.GetProductionPlan(request);
}
