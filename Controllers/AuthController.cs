using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApplication21.Data;
using WebApplication21.Dtos;
using WebApplication21.sakila;

namespace WebApplication21.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public AuthController( IConfiguration config,
            IMapper mapper, 
            UserManager<Users> userManager,
            SignInManager<Users> signInManager)
        {

            _config = config;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewUser([FromBody]  UserForRegisterDto userForRegisterDto)
        {
            var userToCreate = _mapper.Map<Users>(userForRegisterDto);

            var result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.Password);

            var userToReturn = _mapper.Map<UserForDetailedDto>(userToCreate);

            if (result.Succeeded)
            {
                return CreatedAtRoute("GetUser",
                    new { controller = "Users", id = userToCreate.Id }, userToReturn);
            }

            return BadRequest(result.Errors);

        }

        [HttpPost("changePasword")]
        public async Task<IActionResult> changePasword( UserForRegisterDto userForRegisterDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            var username =await _repo.GetUser(userForRegisterDto.Username);
            if (username==null)
            {
                return BadRequest("User name does not exist");
            }

            var userToChange = new Users
            {
                Id= username.Id,
                UserName= userForRegisterDto.Username
            };

            var createUser = await _repo.ChangePassword(userToChange, userForRegisterDto.Password);

            return StatusCode(201);

        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserForLoginDto userForLoginDto)
        {

            var user = await _userManager.FindByNameAsync(userForLoginDto.Username);
            if (user==null)
            {
                return Unauthorized();
            }

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

            if (result.Succeeded)
            {
                var appUser = await _userManager.Users.Include(p => p.Photos)
                    .FirstOrDefaultAsync(u => u.NormalizedUserName == userForLoginDto.Username.ToUpper());

                var userToReturn = _mapper.Map<UserForListDto>(appUser);

                return Ok(new
                {
                    token = GenerateJwtToken(appUser),
                    user = userToReturn
                });
            }

            return Unauthorized();


        }


        private string GenerateJwtToken(Users user)
        {
            //Generate Token
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
            var tokenHandler = new JwtSecurityTokenHandler();

            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[]
                {
                         new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                         new Claim(ClaimTypes.Name,user.UserName)
                }),

                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,


            };


            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenHandler.WriteToken(token);

        }
     }
}