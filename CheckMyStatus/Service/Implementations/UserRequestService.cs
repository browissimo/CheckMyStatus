using CheckMyStatus.DAL;
using CheckMyStatus.Domain.Entity;
using CheckMyStatus.Domain.Enum;
using CheckMyStatus.Domain.Response;
using CheckMyStatus.Domain.ViewModels;
using CheckMyStatus.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CheckMyStatus.Service.Implementations
{
    public class UserRequestService : IUserRequestService
    {
        private readonly IBaseRepository<UserRequest> _userRequestRepository;
        private readonly IBaseRepository<User> _userRepository;

        public UserRequestService(IBaseRepository<UserRequest> userRequestRepository, IBaseRepository<User> userRepository)
        {
            _userRequestRepository = userRequestRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> WriteUserRequest(RequestViewModel requestViewModel)
        {
            int userId = 0;

            var userEmail = requestViewModel.Email;
            var pan = requestViewModel.PAN;

            var userExist = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.Email == userEmail);

            if (userExist != null)
            {
                userId = userExist.Id;
            }

            var userRequestExist = await _userRequestRepository.GetAll().Where((x) => x.UserId == userId && x.pan == pan).FirstOrDefaultAsync();

            if (userRequestExist == null)
            {
                var userRequset = new UserRequest()
                {
                    pan = pan,
                    UserId = userId,
                    CheckDate = requestViewModel.RequserDate,
                    User = _userRepository.GetAll().FirstOrDefault(u => u.Id == userId)
                };

                await _userRequestRepository.Create(userRequset);

                return true;
            }

            return false;

        }
    }
}
