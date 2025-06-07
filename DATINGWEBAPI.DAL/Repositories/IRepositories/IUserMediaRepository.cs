using DATINGWEBAPI.DAL.Entities;

namespace DATINGWEBAPI.DAL.Repositories.IRepositories
{
    public interface IUserMediaRepository
    {
        Task<USER_MEDIum> AddMediaImages(USER_MEDIum uSER_MEDIum);
        Task<USER_STORy> AddStory(USER_STORy uSER_STORy);
    }
}
