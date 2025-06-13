using DATINGWEBAPI.DAL.Entities;

namespace DATINGWEBAPI.DAL.Repositories.IRepositories
{
    public interface IUserMediaRepository
    {
        Task<USER_MEDIum> AddMediaImages(USER_MEDIum uSER_MEDIum);
        Task<USER_STORy> AddStory(USER_STORy uSER_STORy);
        Task<List<USER_MEDIum>> getMediaImages(long userId);
        Task<USER_MEDIum> mediaImagesbyMediaId(string mediaId,long userId);
        Task<bool> DeleteMediaImage(long userId, string mediaId);
    }
}
