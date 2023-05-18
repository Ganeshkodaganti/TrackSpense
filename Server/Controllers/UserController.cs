using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TrackSpense.BL.Contracts;
using TrackSpense.DAL.Models;
using TrackSpense.Shared.BusinessModels;

namespace TrackSpense.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private static IConfiguration _config;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Register")]
    public async Task<bool> RegisterUser([FromBody]Business_User user)
    {
        var UsernameExists = await IsUsernameExists(user.UserName);
        //Encrypting password using BCrpt
        user.Password=BCrypt.Net.BCrypt.HashPassword(user.Password);
        if (UsernameExists == false)
        {
            await _userService.Add(new Business_User()
            {
                UserId = "",
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email
            });
        }
        else
        {
            return false;
        }
        return true;
    }

    [HttpPost("login")]
    public async Task<Business_User> LoginUser(string UserName,string Password)
    {
        var UsernameExists = await IsUsernameExists(UserName);
        if (UsernameExists == false) return null; 

        return await _userService.Get(UserName,Password);

    }

    [HttpGet("IsUsernameExists")]
    public async Task<bool> IsUsernameExists(string Username)
    {
        return await _userService.GetByUserName(Username);
    }


}
