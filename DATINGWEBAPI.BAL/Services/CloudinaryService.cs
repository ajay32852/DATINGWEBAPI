using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DATINGWEBAPI.BAL.Services.IServices;
using DATINGWEBAPI.BAL.Utilities.Common;
using DATINGWEBAPI.BAL.Utilities.Enum;
using DATINGWEBAPI.BLL.Utilities.CustomExceptions;
using DATINGWEBAPI.DAL.Entities;
using DATINGWEBAPI.DAL.Repositories.IRepositories;
using DATINGWEBAPI.DTO.DTOs;
using DATINGWEBAPI.DTO.RequestDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

namespace DATINGWEBAPI.BAL.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CloudinaryService> _localizer;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IUserMediaRepository _userMediaRepository;
        private readonly Cloudinary _cloudinary;
        public CloudinaryService(
            IMapper mapper,
            IStringLocalizer<CloudinaryService> localizer,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            IUserMediaRepository userMediaRepository,
            Cloudinary cloudinary
            )
        {
            _mapper = mapper;
            _localizer = localizer;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _userMediaRepository = userMediaRepository;
            _cloudinary = cloudinary;
        }
        public async Task<UserMediaDTO> AddMediaImages(long userId, MediaUploadRequest mediaUploadRequest)
        {
            // Upload image to Cloudinary
            await using var stream = mediaUploadRequest.File.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(mediaUploadRequest.File.FileName, stream),
                Folder = "DatingApp"
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            if (uploadResult.Error != null)
            {
                throw new Exception(_localizer["Failed to upload image: {0}", uploadResult.Error.Message]);
            }
            var storageId = uploadResult.PublicId?.Replace(UploadStorageNameConst.DatingApp, string.Empty);
            var newMedia = new USER_MEDIum
            {
                USERID = userId,
                MEDIA_URL = uploadResult.SecureUrl.ToString(),
                STORAGE_ID = storageId,
                MEDIA_TYPE = mediaUploadRequest.MediaType,
                IS_PROFILE_PIC = false,
                CREATED_AT = DateTime.UtcNow
            };
            var SaveMediaResponse = await _userMediaRepository.AddMediaImages(newMedia);
            var dto = _mapper.Map<UserMediaDTO>(SaveMediaResponse);
            return dto;
        }


        public async Task<UserStoryDTO> AddStory(long userId, CreateStoryRequestDTO request)
        {

            var (publicId, mediaUrl, mediaType) = await UploadMediaToCloudinaryAsync(request.MediaFile);
            var entity = new USER_STORy
            {
                USERID = userId,
                MEDIA_URL = mediaUrl,
                STORAGE_ID = publicId,
                MEDIA_TYPE = mediaType,
                CAPTION = request.Caption,
                IS_ACTIVE = true,
                EXPIRES_AT = DateTime.UtcNow.AddHours(24),
                CREATED_AT = DateTime.UtcNow
            };
            var savedStory = await _userMediaRepository.AddStory(entity);
            var response = _mapper.Map<UserStoryDTO>(savedStory);
            return response;
        }

        public async Task<(string publicId, string url, string mediaType)> UploadMediaToCloudinaryAsync(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            var isImage = extension == ".jpg" || extension == ".jpeg" || extension == ".png";
            var isVideo = extension == ".mp4" || extension == ".mov" || extension == ".avi";
            if (!isImage && !isVideo)
                throw new Exception("Unsupported media type");
            await using var stream = file.OpenReadStream();
            RawUploadResult uploadResult;
            if (isImage)
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = UploadStorageNameConst.UserStories
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            else // video
            {
                var uploadParams = new VideoUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = UploadStorageNameConst.UserStories
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            if (uploadResult.Error != null)
            {
                throw new Exception(uploadResult.Error.Message);
            }
            var mediaType = isImage ? UploadStorageNameConst.image : UploadStorageNameConst.video;
            return (uploadResult.PublicId, uploadResult.SecureUrl.ToString(), mediaType);
        }


        /// <summary>
        /// get media images for a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<UserMediaDTO>> getMediaImages(long userId)
        {
            var media = await _userMediaRepository.getMediaImages(userId);
            if (media == null)
            {
                throw new NoDataException(_localizer[ResponseMessage.DataNotFound.ToString()]);
            }
            var dto = _mapper.Map<List<UserMediaDTO>>(media);
            return dto;

        }


        public async Task<bool> DeleteMediaImage(long userId, string mediaId)
        {
            var mediaEntity = await _userMediaRepository.mediaImagesbyMediaId(mediaId,userId);
            if (mediaEntity == null)
            {
                throw new NoDataException(_localizer[ResponseMessage.DataNotFound.ToString()]);
            }
            var mediaDto = _mapper.Map<UserMediaDTO>(mediaEntity);
            string storageId = string.Concat(UploadStorageNameConst.DatingApp, mediaDto.StorageId);
            string resourceType = mediaDto.MediaType.ToLowerInvariant();
            var deletionParams = new DeletionParams(storageId);
            if (resourceType == UploadStorageNameConst.video)
            {
                deletionParams.ResourceType = ResourceType.Video;
            }
            else
            {
                deletionParams.ResourceType = ResourceType.Image;
            }

            var result = await _cloudinary.DestroyAsync(deletionParams);
            if (result.Result == "ok" || result.Result == "deleted")
            {
                await _userMediaRepository.DeleteMediaImage(userId,mediaId);
                return true;
            }
            throw new Exception(_localizer[ResponseMessage.Fail.ToString()]);
        }





    }
}
