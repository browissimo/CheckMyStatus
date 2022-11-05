using CheckMyStatus.Domain.Entity;
using CheckMyStatus.Domain.Response;
using CheckMyStatus.Domain.ViewModels;
using CheckMyStatus.Service.Implementations;

namespace CheckMyStatus.Service.Interfaces
{
    public interface IStatusService
    {
        Task<bool> CheckLocalStatus(int pan);
        Task<bool> CheckRemoteStatus(int pan);
        Task<List<RequestChanges>> PrepareRecipients();
    }
}
