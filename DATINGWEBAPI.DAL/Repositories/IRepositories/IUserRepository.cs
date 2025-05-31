using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATINGWEBAPI.DAL.Entities;

namespace DATINGWEBAPI.DAL.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<USER> GetUserByMobileAsync(string mobileNo);
        Task<USER> UpdateLastLogin(USER loginUpdateMap);
    }
}
