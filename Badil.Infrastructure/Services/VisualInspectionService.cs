using Badil.Domain.Interfaces;

namespace Badil.Infrastructure.Services
{
    public class VisualInspectionService : IVisualInspectionAgent
    {
        private static readonly HashSet<string> KnownImageExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp"
        };

        public Task<bool> ValidateImageAsync(string imageUrl, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                return Task.FromResult(false);
            var ext = Path.GetExtension(imageUrl);
            return Task.FromResult(KnownImageExtensions.Contains(ext));
        }

        public Task<string> SuggestMaterialTypeAsync(string imageUrl, CancellationToken ct = default)
        {
            return Task.FromResult(string.Empty);
        }

        public Task<List<string>> AutoTagImageAsync(string imageUrl, CancellationToken ct = default)
        {
            return Task.FromResult(new List<string>());
        }
    }
}
