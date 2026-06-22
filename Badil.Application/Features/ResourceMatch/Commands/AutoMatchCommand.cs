using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Domain.Interfaces;
using MediatR;

namespace Badil.Application.Features.ResourceMatch.Commands
{
    public class AutoMatchCommand : IRequest<int>
    {
    }

    public class AutoMatchCommandHandler : IRequestHandler<AutoMatchCommand, int>
    {
        private readonly IWasteListingRepository _listingRepository;
        private readonly IMaterialRequestRepository _requestRepository;
        private readonly IResourceMatchRepository _matchRepository;
        private readonly ISemanticMatchmaker _matchmaker;
        private readonly INotificationService _notificationService;

        public AutoMatchCommandHandler(
            IWasteListingRepository listingRepository,
            IMaterialRequestRepository requestRepository,
            IResourceMatchRepository matchRepository,
            ISemanticMatchmaker matchmaker,
            INotificationService notificationService)
        {
            _listingRepository = listingRepository;
            _requestRepository = requestRepository;
            _matchRepository = matchRepository;
            _matchmaker = matchmaker;
            _notificationService = notificationService;
        }

        public async Task<int> Handle(AutoMatchCommand request, CancellationToken cancellationToken)
        {
            var activeListings = (await _listingRepository.GetAllAsync(cancellationToken))
                .Where(l => l.Status == Domain.Enum.ListingStatus.Available).ToList();
            var requests = await _requestRepository.GetAllAsync(cancellationToken);
            var matchesCreated = 0;

            foreach (var req in requests)
            {
                var results = await _matchmaker.FindMatchesForRequestAsync(req, activeListings, cancellationToken);
                foreach (var (listing, score) in results.Take(5))
                {
                    var existingMatches = await _matchRepository.GetAllAsync(cancellationToken);
                    if (existingMatches.Any(m => m.ListingId == listing.Id && m.RequestId == req.Id))
                        continue;

                    var match = new Domain.Entity.ResourceMatch
                    {
                        ListingId = listing.Id,
                        RequestId = req.Id,
                        SemanticCompatibilityScore = score,
                        DistanceKm = 0
                    };
                    await _matchRepository.AddAsync(match, cancellationToken);
                    await _notificationService.SendMatchNotificationAsync(req.UserId, match.Id, listing.MaterialType);
                    matchesCreated++;
                }
            }

            return matchesCreated;
        }
    }
}
