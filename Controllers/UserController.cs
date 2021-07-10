using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AuthApi.Data;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository){
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public IActionResult Login( UserLogin userLogin)
        {
            var userFound = _userRepository.GetUserByEmail(userLogin.Email, userLogin.Password);
            if(userFound == null )
            {
                return BadRequest(new {
                    message = "email or password wrong",
                    receivedData = userLogin
                });
            }
           var login =  _userRepository.SignIn(userLogin);
            return Ok(login);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserLogin userLogin)
        {
           var userExist =  _userRepository.GetAllUsers().ToList().Any(user => user.Email == userLogin.Email);

            if( userExist) {
                 return BadRequest(new {
                    message = $"{userLogin.Email} is is being used by another user",
                    receivedData = userLogin
                });
            }
            _userRepository.SignUp(userLogin);
            return Ok( new {
                message = "User created",
                receivedData = userLogin
            });
        }

        [HttpGet("test")]
        [Authorize]
        public IActionResult GetData() {
            return Ok(new {
                message =  "All ok, you have the token"
            });
        }
    }
}