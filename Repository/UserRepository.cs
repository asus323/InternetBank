using System.Threading.Tasks;
using InternetBank.Data;
using InternetBank.Data.User;
using Microsoft.AspNetCore.Identity;

namespace InternetBank.Repository
{
  public class UserRepository :IUserRepository
  {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
    {
            _userManager = userManager;
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

  }
}