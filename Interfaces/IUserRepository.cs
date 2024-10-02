using System.Threading.Tasks;
using InternetBank.Data;
using InternetBank.Data.User;
using InternetBank.Model;
using InternetBank.Model.User;
using Microsoft.AspNetCore.Identity;
namespace InternetBank.Interfaces{
public interface IUserRepository
    {
        Task<IdentityResult> SingUp(SingUpDTO singUpDTO);
        Task<string> LogIn(LogInDTO logInDTO);
    }
}