using CheckMyStatus.Domain.ViewModels;
using CheckMyStatus.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CheckMyStatus.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IStatusService _statusService;
        private readonly IUserRequestService _userRequestService;

        public HomeController(IUserService userService, IStatusService statusService, IUserRequestService userRequestService)
        {
            _userService = userService;
            _statusService = statusService;
            _userRequestService = userRequestService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ChecStatus(RequestViewModel requestViewModel)
        {
            var pan = requestViewModel.PAN;

            var user = new UserViewModel
            {
                Email = requestViewModel.Email
            };

            var createdUser = _userService.Create(user).Result;

            var localStaus = await _statusService.CheckLocalStatus(pan);
            var remoteStatus = await _statusService.CheckRemoteStatus(pan);
            
            var org = new OrganizationViewModel
            {
                pan = requestViewModel.PAN,
                LocalStatus = localStaus,
                PortalStatus = remoteStatus,
                CheckDate = DateTime.Now
            };

            await _userRequestService.WriteUserRequest(requestViewModel);

            requestViewModel.Organizations.Add(org);

            return View("Index", requestViewModel);
        }
    }
}