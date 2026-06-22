using Badil.Domain.Entity;
using Badil.Domain.Enum;
using Badil.Domain.Interfaces;

namespace Badil.Infrastructure.Services
{
    public class SemanticMatchmakerService : ISemanticMatchmaker
    {
        public Task<List<(WasteListing listing, double score)>> FindMatchesForRequestAsync(MaterialRequest request, List<WasteListing> candidates, CancellationToken ct = default)
        {
            var results = candidates
                .Where(l => l.Status == Badil.Domain.Enum.ListingStatus.Available)
                .Select(l =>
                {
                    var keywordScore = ComputeKeywordSimilarity(request.MaterialType, l.MaterialType);
                    var quantityScore = l.Quantity >= request.TargetQuantity ? 1.0 : l.Quantity / request.TargetQuantity;
                    var combinedScore = (keywordScore * 0.7) + (quantityScore * 0.3);
                    return (listing: l, score: combinedScore);
                })
                .Where(r => r.score > 0.3)
                .OrderByDescending(r => r.score)
                .ToList();

            return Task.FromResult(results);
        }

        public Task<List<(MaterialRequest request, double score)>> FindRequestsForListingAsync(WasteListing listing, List<MaterialRequest> candidates, CancellationToken ct = default)
        {
            var results = candidates
                .Select(r =>
                {
                    var keywordScore = ComputeKeywordSimilarity(r.MaterialType, listing.MaterialType);
                    var quantityScore = listing.Quantity >= r.TargetQuantity ? 1.0 : listing.Quantity / r.TargetQuantity;
                    var combinedScore = (keywordScore * 0.7) + (quantityScore * 0.3);
                    return (request: r, score: combinedScore);
                })
                .Where(r => r.score > 0.3)
                .OrderByDescending(r => r.score)
                .ToList();

            return Task.FromResult(results);
        }

        public Task<double> CalculateCompatibilityScoreAsync(string materialType1, string materialType2, CancellationToken ct = default)
        {
            return Task.FromResult(ComputeKeywordSimilarity(materialType1, materialType2));
        }

        private static double ComputeKeywordSimilarity(string a, string b)
        {
            if (string.IsNullOrWhiteSpace(a) || string.IsNullOrWhiteSpace(b))
                return 0;

            a = a.ToLowerInvariant();
            b = b.ToLowerInvariant();

            if (a == b) return 1.0;
            if (a.Contains(b) || b.Contains(a)) return 0.8;

            var wordsA = a.Split(' ', '-', '_');
            var wordsB = b.Split(' ', '-', '_');
            var common = wordsA.Intersect(wordsB).Count();
            var maxWords = Math.Max(wordsA.Length, wordsB.Length);
            return maxWords > 0 ? (double)common / maxWords * 0.6 : 0;
        }
    }
}
