using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NaijaStartupApp.Data;
using NaijaStartupApp.Helpers;
using static NaijaStartupApp.Models.EmailDTOs;
using static NaijaStartupApp.Models.NsuArgs;
using static NaijaStartupApp.Models.NsuDtos;
using static NaijaStartupApp.Models.NsuVariables;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System.Net;

namespace NaijaStartupApp.Services
{
    public interface IUserService
    {
        Task<GenericResponse> AuthenticateAsync(UserRequest Input);
        Task<GenericResponse> CreateUserAsync(CreateUserRequest Input);
        Task<User> get_User(string user);
        User get_User_By_EmailOrUsername(string emailOrUsername);
        Task<GenericResponse> ChangePasswordAsync(User user, string CurrentPassword, string NewPassword);
        Task<User> get_User_By_Session();
        Task<bool> UserExists(string Id);
        Task<User> get_admin_user(string Id);
        Task<User> get_customer(string Id);
        Task<string> get_name(string Id);
        Task<bool> IsUserCustomer(string Id);
        Task<bool> IsUserAdmin(string Id);
        Task<bool> IsUserCustomerEmail(string email);
        Task<bool> IsUserAdminWithEmail(string email);
        Task<bool> sendToManyEmailWithMessage(List<string> ReceiverAddress, List<string> EmailAddress, string subject, string message, string adminMessage);
        EmailResponse Send(EmailMessage emailMessage);
        Task<bool> sendEmailWithMessageAsync(string EmailAddress, string subject, string message);
        Task<List<string>> GetAllAdminEmails();
    }
    public class UserService : IUserService
    {
        private readonly SignInManager<User> _signInManager;
        private ApplicationDbContext _context;
        private UserManager<User> _userManager;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _hcontext;
        private GlobalVariables _globalVariables;
        private TemporaryVariables _temporaryVariables;
        private readonly AppSettings _appSettings;
        private readonly IEmailConfiguration _emailConfiguration;
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {

            new User { Email = "dangote@gmail.com", PasswordHash = "test" }
        };

        public UserService(UserManager<User> userManager,
                            ApplicationDbContext context,
                            SignInManager<User> signInManager,
                            ILogger<UserService> logger,
                            IHttpContextAccessor hcontext,
                            IOptions<AppSettings> appSettings,
                             IEmailConfiguration emailConfiguration
                            )
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _hcontext = hcontext;
            _globalVariables = hcontext.HttpContext.Session.GetObject<GlobalVariables>("GlobalVariables");
            _temporaryVariables = hcontext.HttpContext.Session.GetObject<TemporaryVariables>("TemporaryVariables");
            _appSettings = appSettings.Value;
            _emailConfiguration = emailConfiguration;
            _logger = logger;

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
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                IsActive = true,
                Role = Input.Role,
                CreationTime = DateTime.Now,
                ModificationTime = DateTime.Now,
                DeletionTime = DateTime.Now,
                IsDeleted = false
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

        public async Task<GenericResponse> ChangePasswordAsync(User user, string CurrentPassword, string NewPassword)
        {
            if (user != null || !string.IsNullOrWhiteSpace(CurrentPassword) || !string.IsNullOrWhiteSpace(NewPassword))
            {
                var result = await _userManager.ChangePasswordAsync(user, CurrentPassword, NewPassword);
                if (result.Succeeded)
                {
                    return new GenericResponse { IsSuccessful = true, Message = "User Password Changed Successfully", Error = new List<string> { "" } };
                }
                foreach (var error in result.Errors)
                {
                    return new GenericResponse { IsSuccessful = false, Message = "", Error = new List<string> { error.Description } };
                }
            }
            else
            {
                return new GenericResponse { IsSuccessful = false, Message = "Invalid Request Sent", Error = new List<string> { } };
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

        public async Task<User> get_User(string user)
        {
            return await _context.User.FindAsync(user);
        }
        public async Task<bool> IsUserAdmin(string Id)
        {
            var status = false;
            var user = await _context.User.Where(x => x.Id == Id && x.IsDeleted == false && x.Role.ToLower() == "admin").FirstOrDefaultAsync();
            if (user != null)
                status = true;
            return status;

        }
        public async Task<bool> IsUserCustomer(string Id)
        {
            var status = false;
            var user = await _context.User.Where(x => x.Id == Id && x.IsDeleted == false && x.Role.ToLower() == "user").FirstOrDefaultAsync();
            if (user != null)
                status = true;
            return status;

        }
        public async Task<bool> IsUserAdminWithEmail(string email)
        {
            var status = false;
            var user = await _context.User.Where(x => x.Id == email && x.IsDeleted == false && x.Role.ToLower() == "admin").FirstOrDefaultAsync();
            if (user != null)
                status = true;
            return status;

        }
        public async Task<bool> IsUserCustomerEmail(string email)
        {
            var status = false;
            var user = await _context.User.Where(x => x.Id == email && x.IsDeleted == false && x.Role.ToLower() == "user").FirstOrDefaultAsync();
            if (user != null)
                status = true;
            return status;

        }
        public User get_User_By_EmailOrUsername(string emailOrUsername)
        {
            return _context.User.Where(x=>x.Email ==emailOrUsername || x.UserName == emailOrUsername).FirstOrDefault();
        }
        public async Task<User> get_User_By_Session()
        {
            return await _context.User.FindAsync(_globalVariables.userid);
        }
        public async Task<User> get_admin_user(string user)
        {
            return await _context.User.Where(x => x.Id == user && x.IsDeleted == false && x.Role.ToLower() == "admin").FirstOrDefaultAsync();

        }

        public async Task<string> get_name(string Id)
        {
            User user = await _context.User.Where(x => x.Id == Id && x.IsDeleted == false && x.Role.ToLower() == "admin").FirstOrDefaultAsync();
            string Name = "";
            if (user != null)
            {
                Name = user.FirstName + " " + user.LastName;
            }
            return Name;

        }

        public async Task<User> get_customer(string user)
        {
            return await _context.User.Where(x => x.Id == user && x.IsDeleted == false && x.Role.ToLower() == "user").FirstOrDefaultAsync();
        }
        public async Task<bool> UserExists(string Id)
        {
            var status = false;
            var user = await _context.User.FindAsync(_globalVariables.userid);
            if (user != null) status = true;
            else status = false;
            return status;
        }
        public async Task<List<string>> GetAllAdminEmails()
        {
            List<string> emailLists = new List<string>();
            emailLists = await _context.User.Where(x => x.IsDeleted == false && x.Role.ToLower().Equals("admin")).Select(x => x.Email).ToListAsync();
            return emailLists;
        }
        public async Task<bool> sendToManyEmailWithMessage(List<string> ReceiverAddress, List<string> EmailAddress, string subject, string message, string adminMessage)
        {
            User users, admins;
            string companyName = "";
            string Email = "";
            List<EmailAddress> emailMessageList = new List<EmailAddress>();
            List<EmailAddress> emailMessageListSend = new List<EmailAddress>();
            List<EmailAddress> Receivers = new List<EmailAddress>();
            foreach (var item in EmailAddress)
            {


                try
                {
                    if (await IsUserAdminWithEmail(item))
                    {
                        admins = _context.User.Where(x => x.Email.ToUpper() == item.ToUpper() && x.Role.ToLower().Equals("admin") && x.IsDeleted == false).FirstOrDefault();
                        if (admins == null)
                        {
                            return false;
                        }
                        companyName = admins.FirstName;
                        Email = admins.Email;
                    }
                    else
                    {
                        users = _context.User.SingleOrDefault(x => x.Email.ToUpper() == item.ToUpper() && x.Role.ToLower().Equals("user") && x.IsDeleted == false);
                        if (users == null)
                        {
                            return false;
                        }
                        companyName = users.FirstName + " " + users.LastName;
                        Email = users.Email;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }



                var emailAddress = new EmailAddress
                {
                    Name = companyName,
                    Address = Email
                };
                emailMessageList.Add(emailAddress);


            }
            foreach (var item in ReceiverAddress)
            {


                try
                {
                    if (await IsUserAdminWithEmail(item))
                    {
                        admins = _context.User.Where(x => x.Email.ToUpper() == item.ToUpper() && x.Role.ToLower().Equals("admin") && x.IsDeleted == false).FirstOrDefault();
                        if (admins == null)
                        {
                            return false;
                        }
                        companyName = admins.FirstName + " " + admins.LastName; ;
                        Email = admins.Email;
                    }
                    else
                    {
                        users = _context.User.SingleOrDefault(x => x.Email.ToUpper() == item.ToUpper() && x.Role.ToLower().Equals("user") && x.IsDeleted == false);
                        if (users == null)
                        {
                            return false;
                        }
                        companyName = users.FirstName + " " + users.LastName;
                        Email = users.Email;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }

                var emailAddress = new EmailAddress
                {
                    Name = companyName,
                    Address = Email
                };
                Receivers.Add(emailAddress);


            }
            var emailAddressSend = new EmailAddress
            {
                Name = _appSettings.AdminMessagingDisplayName,
                Address = _appSettings.AdminMessagingEmail
            };

            emailMessageListSend.Add(emailAddressSend);
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var emailMessage = new EmailMessage
            {
                ToAddresses = Receivers,
                Subject = subject,
                Content = message,
                FromAddresses = emailMessageListSend

            };

            var emailResponse = Send(emailMessage);
            if (emailResponse.Code != 200)
            {
                return false;
            }
            emailMessage = new EmailMessage
            {
                ToAddresses = emailMessageList,
                Subject = subject,
                Content = adminMessage,
                FromAddresses = emailMessageListSend

            };
            emailResponse = Send(emailMessage);
            if (emailResponse.Code != 200)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> sendEmailWithMessageAsync(string EmailAddress, string subject, string message)
        {
            var Name = "";
            User users = null;
            try
            {
                var company = await _context.User.Where(x => x.Email.ToUpper() == EmailAddress.ToUpper() && x.IsDeleted == false).FirstOrDefaultAsync();
                if (company != null)
                {
                    Name = company.FirstName + " " + company.LastName;
                    users = company;
                }
                else
                {
                    var admin = await _context.User.Where(x => x.Email.ToUpper() == EmailAddress.ToUpper() && x.IsDeleted == false).FirstOrDefaultAsync();
                    if (admin != null)
                    {
                        Name = admin.FirstName + " " + admin.LastName;
                        users = admin;
                    }
                }

            }
            catch (Exception ex)
            {
                return false;
            }
            if (users == null)
            {
                return false;
            }

            List<EmailAddress> emailMessageList = new List<EmailAddress>();
            List<EmailAddress> emailMessageListSend = new List<EmailAddress>();

            var emailAddress = new EmailAddress
            {
                Name = Name,
                Address = users.Email
            };
            var emailAddressSend = new EmailAddress
            {
                Name = _appSettings.AdminMessagingDisplayName,
                Address = _appSettings.AdminMessagingEmail
            };
            emailMessageList.Add(emailAddress);
            emailMessageListSend.Add(emailAddressSend);
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var emailMessage = new EmailMessage
            {
                ToAddresses = emailMessageList,
                Subject = subject,
                Content = message,
                FromAddresses = emailMessageListSend

            };

            var emailResponse = Send(emailMessage);
            if (emailResponse.Code != 200)
            {
                return false;
            }
            return true;
        }


        public List<EmailMessage> ReceiveEmail(int maxCount = 10)
        {
            using (var emailClient = new Pop3Client())
            {
                emailClient.Connect(_emailConfiguration.PopServer, _emailConfiguration.PopPort, true);

                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                emailClient.Authenticate(_emailConfiguration.PopUsername, _emailConfiguration.PopPassword);

                List<EmailMessage> emails = new List<EmailMessage>();
                for (int i = 0; i < emailClient.Count && i < maxCount; i++)
                {
                    var message = emailClient.GetMessage(i);
                    var emailMessage = new EmailMessage
                    {
                        Content = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
                        Subject = message.Subject
                    };
                    emailMessage.ToAddresses.AddRange(message.To.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
                    emailMessage.FromAddresses.AddRange(message.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
                }

                return emails;
            }
        }

        public EmailResponse Send(EmailMessage emailMessage)
        {
            var message = new MimeMessage();
            var emailResponse = new EmailResponse();
            message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
            message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

            message.Subject = emailMessage.Subject;
            //We will say we are sending HTML. But there are options for plaintext etc. 
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = emailMessage.Content
            };

            try
            {
                //Be careful that the SmtpClient class is the one from Mailkit not the framework!
                using (var emailClient = new SmtpClient())
                {
                    //The last parameter here is to use SSL (Which you should!)
                    emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);

                    //Remove any OAuth functionality as we won't be using it. 
                    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                    emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

                    emailClient.Send(message);

                    emailClient.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Email Exception");
                _logger.LogError(ex.StackTrace);
                emailResponse.Code = (int)HttpStatusCode.BadRequest;
                emailResponse.Message = ex.Message;
                return emailResponse;
            }
            emailResponse.Code = (int)HttpStatusCode.OK;
            emailResponse.Message = "Successfully Sent";
            return emailResponse;

        }
    }

}
