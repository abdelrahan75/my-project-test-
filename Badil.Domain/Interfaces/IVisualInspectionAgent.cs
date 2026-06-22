namespace Badil.Domain.Interfaces
{
    public interface IVisualInspectionAgent
    {
        Task<bool> ValidateImageAsync(string imageUrl, CancellationToken ct = default);
        Task<string> SuggestMaterialTypeAsync(string imageUrl, CancellationToken ct = default);
        Task<List<string>> AutoTagImageAsync(string imageUrl, CancellationToken ct = default);
    }
}
