using System.Threading.Tasks;
using InternetBank.Data;
using InternetBank.Model;
using Microsoft.AspNetCore.Identity;

public interface IUserRepository
    {
        Task<IdentityResult> SingUp(SingUpDTO singUpDTO);
        Task<string> LogIn(LogInDTO logInDTO);
    }