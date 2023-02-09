using Microsoft.AspNetCore.Mvc;
using MultiServiceDemo.Model;
using MultiServiceDemo.Service;

namespace MultiServiceDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public User GetUser()
        {
            return _userService.GetUser();
        }

    }
}
