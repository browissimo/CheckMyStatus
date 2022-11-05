using CheckMyStatus.Domain;
using CheckMyStatus.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace CheckMyStatus.DAL.Reoisitories
{
	public class OrganizationRepository : IBaseRepository<Organization>
	{
        private readonly ApplicationDbContext _context;

        public OrganizationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task Create(Organization entity)
		{
			throw new NotImplementedException();
		}

		public Task Delete(Organization entity)
		{
			throw new NotImplementedException();
		}

		public IQueryable<Organization> GetAll()
		{
			return _context.Organizations;
		}

		public Task<Organization> Update(Organization entity)
		{
			throw new NotImplementedException();
		}
	}
}
