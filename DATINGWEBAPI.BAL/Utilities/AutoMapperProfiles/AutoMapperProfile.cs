using AutoMapper;
using DATINGWEBAPI.DAL.Entities;
using DATINGWEBAPI.DTO.DTOs;
namespace DATINGWEBAPI.BAL.Utilities.AutoMapperProfiles
{
    public static class AutoMapperProfiles
    {
        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<USER, UserDTO>()
                        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.USERID))
                        .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PHONENUMBER))
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
                        .ForMember(dest => dest.AllowContactAccess, opt => opt.MapFrom(src => src.ALLOWCONTACTACCESS))
                        .ForMember(dest => dest.EnableNotifications, opt => opt.MapFrom(src => src.ENABLENOTIFICATIONS))
                        .ForMember(dest => dest.USERINTERESTs, opt => opt.MapFrom(src => src.USERINTERESTs))
                        .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.LONGITUDE))
                        .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.LONGITUDE))
                        .ForMember(dest => dest.IsProfileComplete, opt => opt.MapFrom(src => src.ISPROFILECOMPLETE))
                        .ForMember(dest => dest.USER_MEDIa, opt => opt.MapFrom(src => src.USER_MEDIa))
                        .ReverseMap();

                CreateMap<VERIFICATIONCODE, VerificationCodeDTO>()
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                        .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PHONENUMBER))
                        .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.CODE))
                        .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom(src => src.EXPIRESAT))
                        .ForMember(dest => dest.IsUsed, opt => opt.MapFrom(src => src.ISUSED))
                        .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CREATEDAT))
                        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.USERID))
                        .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.USER))
                        .ReverseMap();

                CreateMap<USERINTEREST, UserInterestDTO>()
                       .ForMember(dest => dest.UserInterestId, opt => opt.MapFrom(src => src.USERINTERESTID))
                       .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.USERID))
                       .ForMember(dest => dest.USER, opt => opt.MapFrom(src => src.USER))
                       .ForMember(dest => dest.INTEREST, opt => opt.MapFrom(src => src.INTEREST))
                       .ForMember(dest => dest.InterestId, opt => opt.MapFrom(src => src.INTERESTID))
                       .ReverseMap();
                CreateMap<USER_MEDIum, UserMediaDTO>()
                      .ForMember(dest => dest.MediaId, opt => opt.MapFrom(src => src.MEDIAID))
                      .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.USERID))
                      .ForMember(dest => dest.MediaUrl, opt => opt.MapFrom(src => src.MEDIA_URL))
                      .ForMember(dest => dest.StorageId, opt => opt.MapFrom(src => src.STORAGE_ID))
                      .ForMember(dest => dest.MediaType, opt => opt.MapFrom(src => src.MEDIA_TYPE))
                      .ForMember(dest => dest.IsProfilePic, opt => opt.MapFrom(src => src.IS_PROFILE_PIC))
                      .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CREATED_AT))
                      .ReverseMap();

                CreateMap<INTEREST, InterestDTO>()
                    .ForMember(dest => dest.INTERESTID, opt => opt.MapFrom(src => src.INTERESTID))
                    .ForMember(dest => dest.NAME, opt => opt.MapFrom(src => src.NAME))
                    .ForMember(dest => dest.ICONURL, opt => opt.MapFrom(src => src.ICONURL))
                    .ForMember(dest => dest.USERINTERESTs, opt => opt.MapFrom(src => src.USERINTERESTs))
                    .ReverseMap();

                CreateMap<SWIPE, SwipeDTO>()
                    .ForMember(dest => dest.SwiperId, opt => opt.MapFrom(src => src.SWIPERID))
                    .ForMember(dest => dest.SwipedId, opt => opt.MapFrom(src => src.SWIPEDID))
                    .ForMember(dest => dest.Liked, opt => opt.MapFrom(src => src.LIKED))
                    .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.TIMESTAMP))
                    .ForMember(dest => dest.Swiper, opt => opt.MapFrom(src => src.SWIPER))
                    .ForMember(dest => dest.Swiped, opt => opt.MapFrom(src => src.SWIPED))
                    .ReverseMap();

                CreateMap<USER_STORy, UserStoryDTO>()
                  .ForMember(dest => dest.StoryId, opt => opt.MapFrom(src => src.STORYID))
                  .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.USERID))
                  .ForMember(dest => dest.MediaUrl, opt => opt.MapFrom(src => src.MEDIA_URL))
                  .ForMember(dest => dest.StorageId, opt => opt.MapFrom(src => src.STORAGE_ID))
                  .ForMember(dest => dest.MediaType, opt => opt.MapFrom(src => src.MEDIA_TYPE))
                  .ForMember(dest => dest.Caption, opt => opt.MapFrom(src => src.CAPTION))
                  .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IS_ACTIVE))
                  .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom(src => src.EXPIRES_AT))
                  .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CREATED_AT))
                  .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UPDATED_AT))
                  .ForMember(dest => dest.USER, opt => opt.MapFrom(src => src.USER))
                  .ReverseMap();




            }
        }
    }
}
