using DATINGWEBAPI.DAL.DataContext;
using DATINGWEBAPI.DAL.Entities;
using DATINGWEBAPI.DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DATINGWEBAPI.DAL.Repositories
{
    public class SwipeRepository(DatingAPPContext datingAPPContext, IHttpContextAccessor httpContextAccessor)
    : ISwipeRepository
    {
        public async Task<USER> getExistsLikeUserProfileById(long SwipedId)
        {
            return await datingAPPContext.USERs.Where(x => x.USERID == SwipedId).FirstOrDefaultAsync();
        }
        public async Task<SWIPE> getExistsLikeProfileById(long  SwipedId,long userId)
        {
            return await datingAPPContext.SWIPEs.Where(x => x.SWIPEDID == SwipedId && x.SWIPERID==userId).FirstOrDefaultAsync();
        }
        public async Task<List<USER>> SwipeProfile(long userId, int pageNumber, int pageSize)
        {
            // 1. Get users already swiped by current user
            var swipedIds = await datingAPPContext.SWIPEs
                .Where(s => s.SWIPERID == userId)
                .Select(s => s.SWIPEDID)
                .ToListAsync();

            // 2. Get users already matched with current user
            var matchedUserIds = await datingAPPContext.MATCHEs
                .Where(m => m.USER1ID == userId || m.USER2ID == userId)
                .Select(m => m.USER1ID == userId ? m.USER2ID : m.USER1ID)
                .ToListAsync();

            // 3. Combine IDs to exclude
            var excludeIds = swipedIds.Union(matchedUserIds).ToList();

            // 4. Query available swipe profiles
            var query = datingAPPContext.USERs
                .AsQueryable()
                .Where(u => u.USERID != userId)            // not self
                .Where(u => !excludeIds.Contains(u.USERID)) // not already swiped or matched
                .Where(u => u.ROLE != "admin")             // no admin

                // You can add more filters here, e.g., gender, age, etc.

                ;

            var swipeProfiles = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return swipeProfiles;
        }
        public async Task<List<USER>> GetMatchesByUserIdAsync(long userId)
        {
            var matches = await (
                from m in datingAPPContext.MATCHEs
                join u in datingAPPContext.USERs on
                    (m.USER1ID == userId ? m.USER2ID : m.USER1ID) equals u.USERID
                where m.USER1ID == userId || m.USER2ID == userId
                select u
            ).Distinct().ToListAsync();

            return matches;
        }



        public async Task<SWIPE> SwipeProfile(SWIPE swipeEntity)
        {
            await using var transaction = await datingAPPContext.Database.BeginTransactionAsync();
            try
            {
                var existingSwipe = await datingAPPContext.SWIPEs
                    .FirstOrDefaultAsync(x => x.SWIPERID == swipeEntity.SWIPERID && x.SWIPEDID == swipeEntity.SWIPEDID);
                if (existingSwipe != null)
                {
                    existingSwipe.TIMESTAMP = swipeEntity.TIMESTAMP;
                    existingSwipe.LIKED = swipeEntity.LIKED;
                    datingAPPContext.SWIPEs.Update(existingSwipe);
                }
                else
                {
                    await datingAPPContext.SWIPEs.AddAsync(swipeEntity);
                }
                await datingAPPContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return swipeEntity;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("An error occurred while saving swipe data.", ex);
            }
        }

        public async Task<bool> FindMutualMatch(long swiperId, long swipedId)
        {
            var isMutual = await datingAPPContext.SWIPEs.AnyAsync(s =>
                s.SWIPERID == swipedId &&
                s.SWIPEDID == swiperId &&
                s.LIKED == true);
            if (isMutual)
            {
                // Check if match already exists to prevent duplicates
                var existingMatch = await datingAPPContext.MATCHEs.AnyAsync(m =>
                    (m.USER1ID == swiperId && m.USER2ID == swipedId) ||
                    (m.USER1ID == swipedId && m.USER2ID == swiperId));
                if (!existingMatch)
                {
                    var match = new MATCH
                    {
                        USER1ID = swiperId,
                        USER2ID = swipedId,
                        MATCHEDAT = DateTime.UtcNow
                    };
                    await datingAPPContext.MATCHEs.AddAsync(match);
                    await datingAPPContext.SaveChangesAsync();
                }
                return true;
            }
            return false; 
        }
        public async Task<List<USER>> GetMatchesFromMatchesTableAsync(long userId)
        {
            var matchedUsers = await (
                from m in datingAPPContext.MATCHEs
                join u in datingAPPContext.USERs on
                    (m.USER1ID == userId ? m.USER2ID : m.USER1ID) equals u.USERID
                where m.USER1ID == userId || m.USER2ID == userId
                select u
            ).Distinct().ToListAsync();

            return matchedUsers;
        }





    }
}
