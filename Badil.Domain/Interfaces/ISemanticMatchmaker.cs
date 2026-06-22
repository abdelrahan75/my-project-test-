using Badil.Domain.Entity;

namespace Badil.Domain.Interfaces
{
    public interface ISemanticMatchmaker
    {
        Task<List<(WasteListing listing, double score)>> FindMatchesForRequestAsync(MaterialRequest request, List<WasteListing> candidates, CancellationToken ct = default);
        Task<List<(MaterialRequest request, double score)>> FindRequestsForListingAsync(WasteListing listing, List<MaterialRequest> candidates, CancellationToken ct = default);
        Task<double> CalculateCompatibilityScoreAsync(string materialType1, string materialType2, CancellationToken ct = default);
    }
}
