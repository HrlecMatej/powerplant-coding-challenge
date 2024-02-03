using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Engie.CodeChallenge.WebApi.ProductionPlans.Models;

public class ProductionPlanRequest
{
    [Range(0, double.MaxValue)]
    public required double Load { get; set; }
    public required FuelData Fuels { get; set; }
    public required IEnumerable<PowerPlant> PowerPlants { get; set; }
}

public class FuelData
{
    [JsonPropertyName("gas(euro/MWh)")]
    [Range(0, double.MaxValue)]
    public required double GasPrice { get; set; }

    [JsonPropertyName("kerosine(euro/MWh)")]
    [Range(0, double.MaxValue)]
    public required double KerosinePrice { get; set; }

    [JsonPropertyName("co2(euro/ton)")]
    [Range(0, double.MaxValue)]
    public required double CO2Price { get; set; }

    [JsonPropertyName("wind(%)")]
    [Range(0, 100)]
    public required int WindPercentage { get; set; }
}

public class PowerPlant
{
    public required string Name { get; set; }
    public PowerPlantType Type { get; set; }

    [Range(0, 1)]
    public required double Efficiency { get; set; }

    [Range(0, int.MaxValue)]
    public required double Pmin { get; set; }

    [Range(0, int.MaxValue)]
    public required double Pmax { get; set; }
}
