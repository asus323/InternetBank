using System.Threading.Tasks;
using InternetBank.Data;
using Microsoft.AspNetCore.Identity;

public interface IUserRepository
    {
        Task<IdentityResult> SingUp(SingUpDTO singUpDTO);
    }