using IdentityService.Models.Dtos;
using IdentityService.Models.Shared;
using IdentityService.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepo _userRepo;

        public AuthController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Register(UserDtos request)
        {

            var user = _userRepo.RegisterUser(request);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);

        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDtos request)
        {

            var token = _userRepo.LoginUser(request);
            if (token == "")
            {
                return BadRequest(" not found");
            }
            return Ok(token);

        }
    }
}
