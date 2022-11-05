using CheckMyStatus.DAL;
using CheckMyStatus.Domain.Entity;
using CheckMyStatus.Domain.Enum;
using CheckMyStatus.Domain.Response;
using CheckMyStatus.Domain.ViewModels;
using CheckMyStatus.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CheckMyStatus.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;

        public UserService(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IBaseResponse<User>> Create(UserViewModel model)
        {
            try
            {
                var userExist = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.Email == model.Email);

                if (userExist == null)
                {
                    var newUser = new User()
                    {
                        Email = model.Email
                    };

                    await _userRepository.Create(newUser);
                }

                return new BaseResponse<User>()
                {
                    StatusCode = StatusCode.Ok
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }      
    }
}
