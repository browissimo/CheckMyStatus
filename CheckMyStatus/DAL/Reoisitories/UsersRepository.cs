using CheckMyStatus.Domain;
using CheckMyStatus.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace CheckMyStatus.DAL.Reoisitories
{
    public class UsersRepository : IBaseRepository<User>
    {
        private readonly ApplicationDbContext _context;

        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users;
        }

        public async Task<User> Update(User entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
