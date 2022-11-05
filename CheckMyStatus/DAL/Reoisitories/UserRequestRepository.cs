using CheckMyStatus.Domain;
using CheckMyStatus.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace CheckMyStatus.DAL.Reoisitories
{
    public class UserRequestRepository : IBaseRepository<UserRequest>
    {
        private readonly ApplicationDbContext _context;

        public UserRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(UserRequest entity)
        {
            await _context.UserRequests.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task Delete(UserRequest entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserRequest> GetAll()
        {
            return _context.UserRequests;
        }

        public async Task<UserRequest> Update(UserRequest entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
