namespace Badil.Domain.Interfaces
{
    public interface IDynamicPricingAgent
    {
        Task<decimal> CalculateSuggestedPriceAsync(string materialType, double quantity, string condition = "average", CancellationToken ct = default);
        Task<decimal> AdjustPriceForTransportAsync(decimal basePrice, double distanceKm, CancellationToken ct = default);
        Task<decimal> GetMarketPriceTrendAsync(string materialType, CancellationToken ct = default);
    }
}
