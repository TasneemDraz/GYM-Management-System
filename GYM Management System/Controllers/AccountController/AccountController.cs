using GYM_Management_System.DTOS;
using GYM_Management_System.DTOS.Auth;
using GYM_Management_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GYM_Management_System.Controllers.AccountController
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        [HttpPost]

        public async Task<IActionResult> Registration(RegisterUserDto registerUserDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = registerUserDto.UserNmae;
                user.Email = registerUserDto.Email;
                IdentityResult result = await _userManager.CreateAsync(user, registerUserDto.Password);
                if (result.Succeeded)
                {
                    return Ok(new Response { Status = "Success", Message = "User created successfully!" });
                }
                //return BadRequest(result.Errors.FirstOrDefault());
                return BadRequest(new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });


            }
            return BadRequest(new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

        }



        //Check Account Valid "Login" "Post"
        [HttpPost]//api/account/login
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            if (ModelState.IsValid == true)
            {
                //check - create token
                ApplicationUser user = await _userManager.FindByNameAsync(loginUserDto.UserName);
                if (user != null)//user name found
                {
                    bool found = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
                    if (found)
                    {
                        //Claims Token
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); // unique identifier for the token.

                        //get role
                        var roles = await _userManager.GetRolesAsync(user);
                        foreach (var itemRole in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, itemRole));
                        }
                        SecurityKey securityKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));

                        SigningCredentials signincred =
                            new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                        //Create token
                        JwtSecurityToken mytoken = new JwtSecurityToken(
                            issuer: _config["JWT:ValidIssuer"],//url web api
                            audience: _config["JWT:ValidAudiance"],//url consumer angular
                            claims: claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signincred
                            );
                        //return Ok(new
                        //{
                        //    token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                        //    expiration = mytoken.ValidTo
                        //});

                        return Ok(new
                        {
                            Status = "Success",
                            Message = "User logged in successfully!",
                            UserData = new
                            {
                                Email = user.Email,
                                UserName=user.UserName,
                            },
                            Token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            Expiration = mytoken.ValidTo
                        });
                    }
                }
                return Unauthorized(new Response { Status = "Error", Message = "Invalid email or password." });

            }
            return Unauthorized(new Response { Status = "Error", Message = "Invalid email or password." });
        }
    }
}
