using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NaijaStartupApp.Data;
using static NaijaStartupApp.Models.NsuArgs;
using static NaijaStartupApp.Models.NsuDtos;

namespace DangoteCustomerPortal.Services
{
    public interface IUserService
    {
        Task<GenericResponse> AuthenticateAsync(UserRequest Input);
        Task<GenericResponse> CreateUserAsync(CreateUserRequest Input);

    }
    public class UserService : IUserService
    {
        private readonly SignInManager<User> _signInManager;
        private ApplicationDbContext _context;
        private UserManager<User> _userManager;
        private readonly ILogger _logger;
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {

            new User { Email = "dangote@gmail.com", PasswordHash = "test" }
        };

        public UserService(UserManager<User> userManager,
                            ApplicationDbContext context,
                            SignInManager<User> signInManager,
                            ILogger<UserService> logger
                            )
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        /// <summary>
        /// This Method Authenticates a user and generates a Java web Token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GenericResponse> AuthenticateAsync(UserRequest Input)
        {

            var result = await _signInManager.PasswordSignInAsync(Input.EmailOrUsername,
                Input.Password, Input.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                //_logger.LogInformation("User logged in.");
                return new GenericResponse { IsSuccessful = true, Message = "Successful", Error = new List<string> { "" } };
            }
            if (result.IsLockedOut)
            {
                //_logger.LogWarning("User account locked out.");
                return new GenericResponse { IsSuccessful = false, Message = "User account locked out." , Error = new List<string> { "" } };
            }
            else
            {
                //_logger.LogWarning("Invalid login attempt.");

                return new GenericResponse { IsSuccessful = false, Message = "Invalid login attempt.", Error = new List<string> { "" } };
            }
        }


        public async Task<GenericResponse> CreateUserAsync(CreateUserRequest Input)
        {
            var user = new User()
            {
                UserName = Input.UserName,
                Email = Input.Email,
            };
            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                //_logger.LogInformation("User created a new account with password.");

                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callbackUrl = Url.Page(
                //    "/Account/ConfirmEmail",
                //    pageHandler: null,
                //    values: new { userId = user.Id, code = code },
                //    protocol: Request.Scheme);

                //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                //await _signInManager.SignInAsync(user, isPersistent: false);
                return new GenericResponse { IsSuccessful = true, Message = "User created a new account with password.", Error= new List<string> { "" } };
            }
            foreach (var error in result.Errors)
            {
                return new GenericResponse { IsSuccessful = false, Message = "", Error = new List<string> { error.Description } };
            }
            return new GenericResponse { };
        }

        //public async Task<UserResponse> SendEmailVerificationTokenAsync(string emailAddress, string baseUrl,
        //      string verificationCodeParameterName, string emailParameterName, string tenantIdParameterName, int tenantId)
        //{
        //    var user = await _userManager.FindByEmailAsync(emailAddress);
        //    if (user == null)
        //        return new UserResponse { IsSuccessful = false, Message = "No account with specified email. Would you like to register?" };

        //    var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //    user.EmailConfirmationCode = emailConfirmationToken;
        //    await _userManager.UpdateAsync(user);

        //    emailConfirmationToken = HttpUtility.UrlEncode(emailConfirmationToken);

        //    var emailVerificationTemplate =
        //        Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates/EmailVerification.html");
        //    using (var sr = new StreamReader(emailVerificationTemplate))
        //    {
        //        var content = await sr.ReadToEndAsync();
        //        content = content.Replace("%%url%%",
        //            $"{baseUrl.TrimEnd('/')}/?" +
        //            $"{verificationCodeParameterName}={emailConfirmationToken}&" +
        //            $"{emailParameterName}={emailAddress}&" +
        //            $"{tenantIdParameterName}={tenantId}");
        //        await _t24.SendEmailFromSpectaAsync("Welcome To Specta", content, user.EmailAddress);
        //    }
        //}
    }

}
