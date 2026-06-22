using Badil.Domain.Entity;
using Badil.Domain.Interfaces;

namespace Badil.Infrastructure.Services
{
    public class GeospatialAnalysisService : IGeospatialAnalyzer
    {
        public Task<double> CalculateDistanceAsync(GeoLocation origin, GeoLocation destination, CancellationToken ct = default)
        {
            var distance = HaversineDistance(origin.Latitude, origin.Longitude, destination.Latitude, destination.Longitude);
            return Task.FromResult(Math.Round(distance, 2));
        }

        public Task<List<(Company company, double distanceKm)>> FindNearbyCompaniesAsync(GeoLocation center, double radiusKm, List<Company> companies, CancellationToken ct = default)
        {
            var results = companies
                .Where(c => c.Location != null)
                .Select(c =>
                {
                    var dist = HaversineDistance(center.Latitude, center.Longitude, c.Location.Latitude, c.Location.Longitude);
                    return (company: c, distanceKm: dist);
                })
                .Where(r => r.distanceKm <= radiusKm)
                .OrderBy(r => r.distanceKm)
                .ToList();

            return Task.FromResult(results);
        }

        public Task<List<(WasteListing listing, double distanceKm)>> FindNearbyListingsAsync(GeoLocation center, double radiusKm, List<WasteListing> listings, Func<WasteListing, GeoLocation?> getLocation, CancellationToken ct = default)
        {
            var results = listings
                .Select(l =>
                {
                    var loc = getLocation(l);
                    if (loc == null) return (listing: l, distanceKm: double.MaxValue);
                    var dist = HaversineDistance(center.Latitude, center.Longitude, loc.Latitude, loc.Longitude);
                    return (listing: l, distanceKm: dist);
                })
                .Where(r => r.distanceKm <= radiusKm)
                .OrderBy(r => r.distanceKm)
                .ToList();

            return Task.FromResult(results);
        }

        private static double HaversineDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371;
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private static double ToRadians(double degrees) => degrees * Math.PI / 180;
    }
}
