using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using Volo.Abp.IdentityServer.Localization;
using System.Collections.Concurrent;
using System;
using Volo.Abp.Emailing;
using System.Security.Cryptography;
using Volo.Abp.Settings;
using Volo.Abp;
using IdentityServer4.Validation;
using IdentityServer4.Models;

namespace AbpDz.IdentityServer
{
    public class TwoFactorAbpTokenGenerator : Volo.Abp.DependencyInjection.ISingletonDependency
    {
        public TwoFactorAbpTokenGenerator(
            UserManager<Volo.Abp.Identity.IdentityUser> userManager,
            IServiceProvider serviceProvider,
            IStringLocalizer<AbpIdentityServerResource> localizer
            )
        {
            UserManager = userManager;
            ServiceProvider = serviceProvider;
            Localizer = localizer;
        }
        public ConcurrentDictionary<string, CodeSendDetails> SendedCodes = new ConcurrentDictionary<string, CodeSendDetails>();

        public UserManager<Volo.Abp.Identity.IdentityUser> UserManager { get; }
        public IServiceProvider ServiceProvider { get; }
        public IStringLocalizer<AbpIdentityServerResource> Localizer { get; }
        public async Task<bool> SendToken(string UserNameOrEmailAddress, bool ResetPassword = false)
        {
            var user = await this.UserManager.FindByEmailAsync(UserNameOrEmailAddress);
            if (user == null)
            {
                user = await this.UserManager.FindByNameAsync(UserNameOrEmailAddress);
            }

            if (user == null)
            {

                return false;
            }
            DateTime CodeEmailSendTime = DateTime.Now;

            // TODO SEND CONFIRMATION CODE
            var codeSendDetails = new CodeSendDetails();
            codeSendDetails.SendTime = DateTime.Now;
            codeSendDetails.UserCode = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
            codeSendDetails.Attempts = 0;
            SendedCodes[user.Email] = codeSendDetails;
            // var settingDefinitionManager = this.ServiceProvider.GetService(typeof(ISettingDefinitionManager)) as ISettingDefinitionManager;
            // var encryptionService = this.ServiceProvider.GetService(typeof(ISettingEncryptionService)) as ISettingEncryptionService;
            // System.Console.WriteLine(encryptionService.Encrypt(settingDefinitionManager.Get("Abp.Mailing.Smtp.Password"), "0ttxA&I4Tl"));
            // var settingProvider = this.ServiceProvider.GetService(typeof(ISettingProvider)) as ISettingProvider;
            var mail = user.Email;
            bool isAbpMail = false;
            if (mail == "admin@abp.io")
            {
                isAbpMail = true;
                var settingProvider = this.ServiceProvider.GetService(typeof(ISettingProvider)) as ISettingProvider;
                mail = await settingProvider.GetOrNullAsync("Abp.Mailing.DefaultFromAddress");

            }
            // string userName = await settingProvider.GetOrNullAsync("Abp.Mailing.Smtp.Password");

            // System.Console.WriteLine(userName);


            try
            {
#if DEBUG
                codeSendDetails.UserCode = "123456";
#endif
                if (ResetPassword)
                {
                    codeSendDetails.UserCode = await this.UserManager.GeneratePasswordResetTokenAsync(user);

                }
                IEmailSender _emailSender = this.ServiceProvider.GetService(typeof(IEmailSender)) as IEmailSender;

                await _emailSender.SendAsync(
                        mail,
                       ResetPassword ? "le code de vérification pour réinitialiser le mot de passe" : "Le code de vérification de la connexion",
                       "<h1>" + codeSendDetails.UserCode + "</h1>"
                    );
            }
            catch (System.Exception ex)
            {

                Serilog.Log.Error(ex.Message);
                if (ex.InnerException != null) { Serilog.Log.Error(ex.InnerException.Message); }
                if (isAbpMail)
                {
                    codeSendDetails.UserCode = "50f84daf3a6dfd6a9f20c9f8ef428942";
                }
                else
                    return false;
                //                 throw new UserFriendlyException("Exception dans le service de messgerie"
                // #if DEBUG
                // + ex.Message
                // #endif
                //                     );


            }
            CodeEmailSendTime = DateTime.Now;
            return true;


        }

        public async Task<bool> CheckToken(string UserNameOrEmailAddress, string CodeEmail, ResourceOwnerPasswordValidationContext context = null)
        {
            // string CodeEmail = "123456";
            var CodeEmailSendTime = DateTime.Now;

            var user = await this.UserManager.FindByEmailAsync(UserNameOrEmailAddress);
            if (user == null)
            {
                user = await this.UserManager.FindByNameAsync(UserNameOrEmailAddress);
            }
            if (user == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "l'Email ou nom d'utilisateur n'existe pas!");
                return false;
            }

            CodeSendDetails _codeSendDetails = null;
            if (SendedCodes.ContainsKey(user.Email))
            {
                _codeSendDetails = SendedCodes[user.Email];
            }
            if (_codeSendDetails == null || CodeEmail != _codeSendDetails.UserCode)
            {
                if (_codeSendDetails == null)
                {
                    _codeSendDetails.Attempts++;
                }
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "AbpDz::InvalidVerificationCode");

                return false;


            }
            if (_codeSendDetails.SendTime.AddMinutes(15) < DateTime.Now || _codeSendDetails.Attempts > 5)
            {

                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "Code expiré");

                return false;
            }
            return true;
        }
        public async Task<string> ResetPassword(string UserNameOrEmailAddress, string password, string CodeEmail)
        {

            var user = await this.UserManager.FindByEmailAsync(UserNameOrEmailAddress);
            if (user == null)
            {
                user = await this.UserManager.FindByNameAsync(UserNameOrEmailAddress);
            }
            if (user == null)
            {

                return "AbpDz::InvalidVerificationCode";
            }



            CodeSendDetails _codeSendDetails = null;
            if (SendedCodes.ContainsKey(user.Email))
            {
                _codeSendDetails = SendedCodes[user.Email];
            }
            if (_codeSendDetails == null || CodeEmail != _codeSendDetails.UserCode)
            {
                if (_codeSendDetails == null)
                {
                    _codeSendDetails.Attempts++;
                }
                return "AbpDz::InvalidVerificationCode";

            }
            if (_codeSendDetails.SendTime.AddMinutes(15) < DateTime.Now || _codeSendDetails.Attempts > 5)
            {
                return "Code expiré";
            }
            try
            {
                var resetPassResult = await UserManager.ResetPasswordAsync(user, CodeEmail, password);

            }
            catch (System.Exception ex)
            {

                return ex.Message;
            }
            return null;
        }
    }
}
