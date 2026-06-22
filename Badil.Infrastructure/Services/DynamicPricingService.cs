using Badil.Domain.Entity;
using Badil.Domain.Interfaces;

namespace Badil.Infrastructure.Services
{
    public class DynamicPricingService : IDynamicPricingAgent
    {
        private static readonly Dictionary<string, decimal> MarketIndex = new(StringComparer.OrdinalIgnoreCase)
        {
            { "plastic", 250 },
            { "metal", 800 },
            { "wood", 150 },
            { "paper", 100 },
            { "glass", 120 },
            { "textile", 180 },
            { "rubber", 200 },
            { "electronic", 500 },
            { "chemical", 300 },
            { "organic", 80 }
        };

        public Task<decimal> CalculateSuggestedPriceAsync(string materialType, double quantity, string condition = "average", CancellationToken ct = default)
        {
            var basePrice = MarketIndex.TryGetValue(materialType, out var price) ? price : 150m;
            var conditionMultiplier = condition.ToLowerInvariant() switch
            {
                "excellent" => 1.2m,
                "good" => 1.0m,
                "average" => 0.85m,
                "fair" => 0.7m,
                "poor" => 0.5m,
                _ => 0.85m
            };
            var quantityDiscount = quantity > 10 ? 0.9m : quantity > 5 ? 0.95m : 1.0m;
            var suggestedPrice = basePrice * conditionMultiplier * quantityDiscount;
            return Task.FromResult(Math.Round(suggestedPrice, 2));
        }

        public Task<decimal> AdjustPriceForTransportAsync(decimal basePrice, double distanceKm, CancellationToken ct = default)
        {
            var transportCost = (decimal)(distanceKm * 0.5);
            var adjusted = basePrice + transportCost;
            return Task.FromResult(Math.Round(adjusted, 2));
        }

        public Task<decimal> GetMarketPriceTrendAsync(string materialType, CancellationToken ct = default)
        {
            var trend = MarketIndex.TryGetValue(materialType, out var price) ? price : 150m;
            return Task.FromResult(trend);
        }
    }
}
