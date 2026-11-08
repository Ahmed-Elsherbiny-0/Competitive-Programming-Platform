using Application.Comman;
using Application.Interfaces;
using Domain.Comman;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class IdentityService(UserManager<ApplicationUser> _userManager,
        SignInManager<ApplicationUser> _signInManager,
        IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory,
        IAuthorizationService _authorizationService) :IIdentityService
    {

        public async Task<string?> GetUserNameAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return user?.UserName;
        }
        public async Task<ApplicationUser?> FindByEmailAsync(string Email)
        {
            var userIsFound = await _signInManager.UserManager.FindByEmailAsync(Email);
            return userIsFound;
        }
        public async Task<ApplicationUser?> FindByIdAsync(string userId)
        {
            var userIsFound = await _signInManager.UserManager.FindByIdAsync(userId);
            return userIsFound;
        }

        public async Task<IdentityResult > CreateUserAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            return (result);
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

      
        public async Task<Result> DeleteUserAsync(ApplicationUser appuser)
        {
            var user = await _userManager.FindByIdAsync(appuser.Id);

            return user != null ? await DeleteUserAsync(user) : Result.Success();
        }
        public async Task<string > GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var code = await _signInManager.UserManager.GenerateEmailConfirmationTokenAsync(user);
            return code;
        }

        public async  Task<SignInResult> CheckPasswordSignInAsync(ApplicationUser user, string password, bool flag)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, flag);
            return result;
        }
        public async Task UpdateUserAsync(ApplicationUser user)
        {
        await _signInManager.UserManager.UpdateAsync(user);

        }

        public async  Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string code)
        {
            var result = await _signInManager.UserManager.ConfirmEmailAsync(user, code);
            return result;
        }
       public async Task<string>  GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            var code = await _signInManager.UserManager.GeneratePasswordResetTokenAsync(user);
            return code;
        }
        public  async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string code, string newPassword)
        {
                 var result = await _signInManager.UserManager.ResetPasswordAsync(user, code, newPassword);
            return result;
        }

    }
}
