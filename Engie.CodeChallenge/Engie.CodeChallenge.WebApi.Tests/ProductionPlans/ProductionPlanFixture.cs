using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engie.CodeChallenge.WebApi.ProductionPlans;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Microsoft.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace Engie.CodeChallenge.WebApi.Tests.ProductionPlans;

public class ProductionPlanFixture : TestBedFixture
{
    protected override void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IProductionPlanService, ProductionPlanService>();
    }

    protected override IEnumerable<TestAppSettings> GetTestAppSettings() => Array.Empty<TestAppSettings>();

    protected override ValueTask DisposeAsyncCore() => new();
}
