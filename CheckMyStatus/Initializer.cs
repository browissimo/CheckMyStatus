using CheckMyStatus.DAL.Reoisitories;
using CheckMyStatus.DAL;
using CheckMyStatus.Domain.Entity;
using CheckMyStatus.Service.Implementations;
using CheckMyStatus.Service.Interfaces;

namespace CheckMyStatus
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection repository)
        {
            repository.AddScoped<IBaseRepository<User>, UsersRepository>();
            repository.AddScoped<IBaseRepository<Organization>, OrganizationRepository>();
            repository.AddScoped<IBaseRepository<UserRequest>, UserRequestRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IUserRequestService, UserRequestService>();
        }
    }
}
