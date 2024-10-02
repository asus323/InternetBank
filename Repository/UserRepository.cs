using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InternetBank.Data;
using InternetBank.Data.User;
using InternetBank.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace InternetBank.Repository
{
  public class UserRepository :IUserRepository
  {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        public UserRepository(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          IConfiguration configuration
        )
    {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
    public async Task<IdentityResult> SingUp(SingUpDTO singUpDTO){
      var user = new ApplicationUser()
      {
        FirstName = singUpDTO.FirstName,
        LastName = singUpDTO.LastName,
        NationalCode = singUpDTO.NationalCode,
        BirthDate = singUpDTO.BirthDate,
        PhoneNumber = singUpDTO.PhoneNumber,
        Email = singUpDTO.Email,
        UserName = singUpDTO.Email,
      };
      return await _userManager.CreateAsync(user,singUpDTO.Password);

    }
    public async Task<string> LogIn(LogInDTO logInDTO){
      var result = await _signInManager.PasswordSignInAsync(logInDTO.Email,logInDTO.Password,false,false);
      if(!result.Succeeded) return null;
      var authClaim =new List<Claim>(){
        new Claim(ClaimTypes.Email,logInDTO.Email),
        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
      };
      var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
      var token = new JwtSecurityToken(
        issuer : _configuration["JWT:ValidIssuer"],
        audience : _configuration["JWT:ValidAudience"],
        expires : DateTime.Now.AddDays(1),
        claims: authClaim,
        signingCredentials : new SigningCredentials(authSignInKey,SecurityAlgorithms.HmacSha256Signature)
      );
      return new JwtSecurityTokenHandler().WriteToken(token);
    }

  }
}