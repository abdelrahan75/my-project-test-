using AutoMapper;
using Badil.Application.Features.Auth.DTOs;
using Badil.Application.Features.Auth.Commands;
using Badil.Application.Features.Admin.Commands.CreateAdmin;
using Badil.Domain.Entity;

namespace Badil.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, LoginResponse>()
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
            
            CreateMap<RegisterCommand, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CreateAdminCommand, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // --- Marketplace entities -> DTOs ---

            CreateMap<Company, Badil.Application.Features.Company.DTOs.CompanyDto>()
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Location != null ? src.Location.Latitude : 0))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Location != null ? src.Location.Longitude : 0));

            CreateMap<WasteListing, Badil.Application.Features.WasteListing.DTOs.WasteListingDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<MaterialRequest, Badil.Application.Features.MaterialRequest.DTOs.MaterialRequestDto>();

            CreateMap<Message, Badil.Application.Features.Message.DTOs.MessageDto>();

            CreateMap<Notification, Badil.Application.Features.Notification.DTOs.NotificationDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));

            CreateMap<ResourceMatch, Badil.Application.Features.ResourceMatch.DTOs.ResourceMatchDto>();

            CreateMap<Transaction, Badil.Application.Features.Transaction.DTOs.TransactionDto>()
                .ForMember(dest => dest.EscrowState, opt => opt.MapFrom(src => src.EscrowState.ToString()));

            CreateMap<DisputeTicket, Badil.Application.Features.DisputeTicket.DTOs.DisputeTicketDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<VerificationRequest, Badil.Application.Features.VerificationRequest.DTOs.VerificationRequestDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
