using CheckMyStatus.Domain.Entity;
using CheckMyStatus.Domain.Response;
using CheckMyStatus.Domain.ViewModels;

namespace CheckMyStatus.Service.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<User>> Create(UserViewModel user);
        //Task<IBaseResponse<RequestViewModel>> GetUser(string email, int pan);
    }
}
