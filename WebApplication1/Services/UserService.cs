using AuthProject.Models;
using AuthProject.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AuthProject.Services
{
    public interface IUserService
    {
        Task<UserViewModel> CreateAsync(UserCreateModel model);
        Task<UserViewModel> UpdateAsync(long id, UserUpdateModel model);
        Task<UserViewModel> GetByIdAsync(long id);
        Task<bool> ResetPasswordAsync(ResetPasswordDtoModel model);
    }

    public class UserService : IUserService
    {
        private readonly ProjectContext _context;

        public UserService(ProjectContext context)
        {
            _context = context;
        }

        public async Task<UserViewModel> CreateAsync(UserCreateModel model)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(model.Password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = savedPasswordHash
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(user.Id);
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDtoModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user == null) return false;

            return true;
        }

        public async Task<UserViewModel> GetByIdAsync(long id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return null;

            var userView = new UserViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Id = user.Id
            };

            return userView;
        }

        public async Task<UserViewModel> UpdateAsync(long id, UserUpdateModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return null;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            _context.Update(user);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(user.Id);
        }
    }
}
