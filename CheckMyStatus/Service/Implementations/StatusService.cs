using CheckMyStatus.DAL;
using CheckMyStatus.DAL.Reoisitories;
using CheckMyStatus.Domain.Entity;
using CheckMyStatus.Domain.Enum;
using CheckMyStatus.Domain.Response;
using CheckMyStatus.Domain.ViewModels;
using CheckMyStatus.Service.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System;

namespace CheckMyStatus.Service.Implementations
{
    public class StatusService : IStatusService
    {
        private readonly IBaseRepository<Organization> _organizationRepository;
        private readonly IBaseRepository<UserRequest> _userRequestRepository;
        private readonly IBaseRepository<User> _userRepository;
             
        public StatusService(IBaseRepository<Organization> organizationRepository, IBaseRepository<UserRequest> userRequestRepository, IBaseRepository<User> userRepository)
        {
            _organizationRepository = organizationRepository;
            _userRequestRepository = userRequestRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> CheckLocalStatus(int pan)
        {
            var organization = await _organizationRepository.GetAll().FirstOrDefaultAsync(x => x.Pan == pan);
            if (organization == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CheckRemoteStatus(int pan)
        {
            HttpClient client = new HttpClient();

            try
            {
                var request = await client.GetAsync($"https://www.portal.nalog.gov.by/grp/getData?unp={pan}");

                if (request.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task< List<RequestChanges>> PrepareRecipients()
        {

            List<RequestChanges> requserChanges = new();

            var userRequests = _userRequestRepository.GetAll().ToList();

            foreach (var request in userRequests)
            {
                var oldLocalStatus = request.LocalStatus;
                var oldRemoteStatus = request.RemoteStatus;
                var currentLocalStatus = await CheckLocalStatus(request.pan);
                var currentRemoteStatus = await CheckRemoteStatus(request.pan);

                var t1 = 1;

                if ((oldLocalStatus != currentLocalStatus) || (oldRemoteStatus != currentRemoteStatus))
                {
                    var requestChanges = new RequestChanges() { Pan = request.pan, UserId = request.UserId };

                    var requestChangesUser =  _userRepository.GetAll().FirstOrDefault(u => u.Id == request.UserId);

                    requestChanges.Email = requestChangesUser.Email;

                    if (oldLocalStatus != currentLocalStatus)
                    {
                        requestChanges.Local = true;
                    }

                    if (oldRemoteStatus != currentRemoteStatus)
                    {
                        requestChanges.Remote = true;
                    }

                    requserChanges.Add(requestChanges);
                }

            }

            return requserChanges;
        }

    }
}
