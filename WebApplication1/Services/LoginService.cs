using AuthProject.Models;
using AuthProject.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AuthProject.Services
{
    public interface ILoginService
    {
        Task<bool> LoginAsync(LoginAndPasswordModel model);
    }

    public class LoginService : ILoginService
    {
        private readonly ProjectContext _context;

        public LoginService(ProjectContext context)
        {
            _context = context;
        }

        public async Task<bool> LoginAsync(LoginAndPasswordModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null) return false;

            var savedPasswordHash = user.Password;
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(model.Password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;

            return true;
        }
    }
}
