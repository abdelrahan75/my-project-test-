using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Badil.Application.Features.WasteListing.Commands
{
    public class UploadWasteListingImageCommand : IRequest<string>
    {
        public Guid ListingId { get; set; }
        public IFormFile File { get; set; }
    }

    public class UploadWasteListingImageCommandHandler : IRequestHandler<UploadWasteListingImageCommand, string>
    {
        private readonly IWasteListingRepository _repository;
        private readonly IFileService _fileService;
        private readonly ICurrentUserService _currentUserService;

        public UploadWasteListingImageCommandHandler(IWasteListingRepository repository, IFileService fileService, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _fileService = fileService;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(UploadWasteListingImageCommand request, CancellationToken cancellationToken)
        {
            var listing = await _repository.GetByIdAsync(request.ListingId, cancellationToken);
            if (listing == null)
                throw new KeyNotFoundException("Waste listing not found");
            if (listing.UserId != _currentUserService.UserId && !_currentUserService.IsAdmin)
                throw new UnauthorizedAccessException("You are not allowed to modify this listing.");

            var filePath = await _fileService.UploadFileAsync(request.File, "waste-images");
            listing.ImageUrls.Add(filePath);
            await _repository.UpdateAsync(listing, cancellationToken);
            return filePath;
        }
    }
}
