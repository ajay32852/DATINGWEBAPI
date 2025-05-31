using AutoMapper;
using DATINGWEBAPI.DAL.Entities;
using DATINGWEBAPI.DTO.DTOs;
using DATINGWEBAPI.DTO.RequestDTO;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace DATINGWEBAPI.BAL.Utilities.AutoMapperProfiles
{
    public static class AutoMapperProfiles
    {
        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                 /*USER MAP DTO*/
                CreateMap<USER, UserDTO>()
                        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.USERID))
                        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EMAIL))
                        .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PHONENUMBER))
                        .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PASSWORDHASH))
                        .ForMember(dest => dest.AuthProvider, opt => opt.MapFrom(src => src.AUTH_PROVIDER))
                        .ForMember(dest => dest.ProviderId, opt => opt.MapFrom(src => src.PROVIDERID))
                        .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FIRSTNAME))
                        .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LASTNAME))
                        .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.GENDER))
                        .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.BIRTHDAY))
                        .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.AGE))
                        .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.BIO))
                        .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.LOCATION))
                        .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.PROFILEIMAGEURL))
                        .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.ROLE))
                        .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.ISDELETED))
                        .ForMember(dest => dest.DeletedAt, opt => opt.MapFrom(src => src.DELETEDAT))
                        .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CREATEDAT))
                        .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UPDATEDAT))
                        .ForMember(dest => dest.LastLogin, opt => opt.MapFrom(src => src.LASTLOGIN))
                        .ForMember(dest => dest.IsBlocked, opt => opt.MapFrom(src => src.ISBLOCKED))
                        .ReverseMap();

                        /**/





            }
        }
    }
}
