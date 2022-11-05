using CheckMyStatus.Domain.Entity;
using CheckMyStatus.Domain.Response;
using CheckMyStatus.Domain.ViewModels;

namespace CheckMyStatus.Service.Interfaces
{
    public interface IUserRequestService
    {
        Task<bool> WriteUserRequest(RequestViewModel responseViewModel);
    }
}
