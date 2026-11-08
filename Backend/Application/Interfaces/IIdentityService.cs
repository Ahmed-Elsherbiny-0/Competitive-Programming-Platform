using Domain.Comman;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IIdentityService
    {
        Task<string?> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<Result> DeleteUserAsync(ApplicationUser appuser);

        Task<ApplicationUser?> FindByEmailAsync(string Email);
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task<SignInResult> CheckPasswordSignInAsync(ApplicationUser user, string password, bool flag);
        public Task UpdateUserAsync(ApplicationUser user);
        public Task<ApplicationUser?> FindByIdAsync(string userId);
         public Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string code);
        public Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
       public Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string code, string newPassword);
    }
}
