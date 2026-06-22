using Badil.Domain.Entity;

namespace Badil.Domain.Interfaces
{
    public interface IGeospatialAnalyzer
    {
        Task<double> CalculateDistanceAsync(GeoLocation origin, GeoLocation destination, CancellationToken ct = default);
        Task<List<(Company company, double distanceKm)>> FindNearbyCompaniesAsync(GeoLocation center, double radiusKm, List<Company> companies, CancellationToken ct = default);
        Task<List<(WasteListing listing, double distanceKm)>> FindNearbyListingsAsync(GeoLocation center, double radiusKm, List<WasteListing> listings, Func<WasteListing, GeoLocation?> getLocation, CancellationToken ct = default);
    }
}
