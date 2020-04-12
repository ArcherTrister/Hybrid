using Hybrid.AspNetCore.Mvc;
using Hybrid.Identity.Entities;
using Hybrid.Net;
using Hybrid.Zero.IdentityServer4.Quickstart.Helpers;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using System;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Hybrid.Zero.IdentityServer4.Quickstart
{
    [HybridDefaultUI(typeof(ManageController<,>))]
    public abstract class ManageController : IdentityServerBaseController
    {
    }

    [SecurityHeaders]
    [Area("IdentityServer")]
    [Route("IdentityServer/Manage")]
    [Authorize]
    public class ManageController<TUser, TKey> : ManageController
    where TUser : UserBase<TKey>, new()
    where TKey : IEquatable<TKey>
    {
        private readonly UserManager<TUser> _userManager;
        private readonly SignInManager<TUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ManageController<TUser, TKey>> _logger;
        //private readonly IGenericControllerLocalizer<ManageController<TUser, TKey>> L;
        private readonly UrlEncoder _urlEncoder;

        private const string RecoveryCodesKey = nameof(RecoveryCodesKey);
        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        [TempData]
        public string StatusMessage { get; set; }

        public ManageController(UserManager<TUser> userManager, SignInManager<TUser> signInManager, 
            IEmailSender emailSender, ILogger<ManageController<TUser, TKey>> logger, 
            //IGenericControllerLocalizer<ManageController<TUser, TKey>> localizer, 
            UrlEncoder urlEncoder)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            //L = localizer;
            _urlEncoder = urlEncoder;
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            var model = await BuildManageIndexViewModelAsync(user);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Index")]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            var email = user.Email;
            if (model.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException(L("ErrorSettingEmail", user.Id));
                }
            }

            var phoneNumber = user.PhoneNumber;
            if (model.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    throw new ApplicationException(L("ErrorSettingPhone", user.Id));
                }
            }

            await UpdateUserClaimsAsync(model, user);

            StatusMessage = L("ProfileUpdated");

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("SendVerificationEmail")]
        public async Task<IActionResult> SendVerificationEmail(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code }, HttpContext.Request.Scheme);

            await _emailSender.SendAsync(model.Email, L("ConfirmEmailTitle"), L("ConfirmEmailBody", HtmlEncoder.Default.Encode(callbackUrl)));

            StatusMessage = L("VerificationSent");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToAction(nameof(SetPassword));
            }

            var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation(L("PasswordChangedLog", user.UserName));

            StatusMessage = L("PasswordChanged");

            return RedirectToAction(nameof(ChangePassword));
        }

        [HttpGet]
        [Route("SetPassword")]
        public async Task<IActionResult> SetPassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return RedirectToAction(nameof(ChangePassword));
            }

            var model = new SetPasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("SetPassword")]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                AddErrors(addPasswordResult);
                return View(model);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = L("PasswordSet");

            return RedirectToAction(nameof(SetPassword));
        }

        [HttpGet]
        [Route("PersonalData")]
        public async Task<IActionResult> PersonalData()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("DownloadPersonalData")]
        public async Task<IActionResult> DownloadPersonalData()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            _logger.LogInformation(L("AskForPersonalDataLog"), _userManager.GetUserId(User));

            var personalDataProps = typeof(TUser).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            var personalData = personalDataProps.ToDictionary(p => p.Name, p => p.GetValue(user)?.ToString() ?? "null");

            Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");
            return new FileContentResult(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(personalData)), "text/json");
        }

        [HttpGet]
        [Route("DeletePersonalData")]
        public async Task<IActionResult> DeletePersonalData()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            var deletePersonalDataViewModel = new DeletePersonalDataViewModel
            {
                RequirePassword = await _userManager.HasPasswordAsync(user)
            };

            return View(deletePersonalDataViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("DeletePersonalData")]
        public async Task<IActionResult> DeletePersonalData(DeletePersonalDataViewModel deletePersonalDataViewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            deletePersonalDataViewModel.RequirePassword = await _userManager.HasPasswordAsync(user);
            if (deletePersonalDataViewModel.RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, deletePersonalDataViewModel.Password))
                {
                    ModelState.AddModelError(string.Empty, L("PasswordNotCorrect"));
                    return View(deletePersonalDataViewModel);
                }
            }

            var result = await _userManager.DeleteAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(L("ErrorDeletingUser", user.Id));
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation(L("DeletePersonalData"), userId);

            return Redirect("~/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("RemoveLogin")]
        public async Task<IActionResult> RemoveLogin(RemoveLoginViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            var result = await _userManager.RemoveLoginAsync(user, model.LoginProvider, model.ProviderKey);
            if (!result.Succeeded)
            {
                throw new ApplicationException(L("ErrorRemovingExternalLogin", user.Id));
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = L("ExternalLoginRemoved");

            return RedirectToAction(nameof(ExternalLogins));
        }

        [HttpGet]
        [Route("LinkLoginCallback")]
        public async Task<IActionResult> LinkLoginCallback()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            var info = await _signInManager.GetExternalLoginInfoAsync(user.Id.ToString());
            if (info == null)
            {
                throw new ApplicationException(L("ErrorLoadingExternalLogin", user.Id));
            }

            var result = await _userManager.AddLoginAsync(user, info);
            if (!result.Succeeded)
            {
                AddErrors(result);
                return View("LinkLoginFailure");
            }

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            StatusMessage = L("ExternalLoginAdded");

            return RedirectToAction(nameof(ExternalLogins));
        }

        [HttpGet]
        [Route("ExternalLogins")]
        public async Task<IActionResult> ExternalLogins()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            var model = new ExternalLoginsViewModel
            {
                CurrentLogins = await _userManager.GetLoginsAsync(user)
            };

            model.OtherLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync())
                .Where(auth => model.CurrentLogins.All(ul => auth.Name != ul.LoginProvider))
                .ToList();

            model.ShowRemoveButton = await _userManager.HasPasswordAsync(user) || model.CurrentLogins.Count > 1;
            model.StatusMessage = StatusMessage;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("LinkLogin")]
        public async Task<IActionResult> LinkLogin(string provider)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Action(nameof(LinkLoginCallback));
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));

            return new ChallengeResult(provider, properties);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("GenerateRecoveryCodes")]
        public async Task<IActionResult> GenerateRecoveryCodes()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            if (!user.TwoFactorEnabled)
            {
                AddError(L("ErrorGenerateCodesWithout2FA"));
                return View();
            }

            var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);

            _logger.LogInformation(L("UserGenerated2FACodes", user.Id));

            var model = new ShowRecoveryCodesViewModel { RecoveryCodes = recoveryCodes.ToArray() };

            return View(nameof(ShowRecoveryCodes), model);
        }

        [HttpGet]
        [Route("ShowRecoveryCodes")]
        public IActionResult ShowRecoveryCodes()
        {
            var recoveryCodes = (string[])TempData[RecoveryCodesKey];
            if (recoveryCodes == null)
            {
                return RedirectToAction(nameof(TwoFactorAuthentication));
            }

            var model = new ShowRecoveryCodesViewModel { RecoveryCodes = recoveryCodes };

            return View(model);
        }

        [HttpGet]
        [Route("TwoFactorAuthentication")]
        public async Task<IActionResult> TwoFactorAuthentication()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            var model = new TwoFactorAuthenticationViewModel
            {
                HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null,
                Is2faEnabled = user.TwoFactorEnabled,
                RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user),
                IsMachineRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user)
            };

            return View(model);
        }

        [HttpPost]
        [Route("ForgetTwoFactorClient")]
        public async Task<IActionResult> ForgetTwoFactorClient()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            await _signInManager.ForgetTwoFactorClientAsync();

            StatusMessage = L("SuccessForgetBrowser2FA");

            return RedirectToAction(nameof(TwoFactorAuthentication));
        }

        [HttpGet]
        [Route("Disable2faWarning")]
        public async Task<IActionResult> Disable2faWarning()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            if (!user.TwoFactorEnabled)
            {
                throw new ApplicationException(L("ErrorDisable2FA", user.Id));
            }

            return View(nameof(Disable2fa));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Disable2fa")]
        public async Task<IActionResult> Disable2fa()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            var disable2faResult = await _userManager.SetTwoFactorEnabledAsync(user, false);
            if (!disable2faResult.Succeeded)
            {
                throw new ApplicationException(L("ErrorDisable2FA", user.Id));
            }

            _logger.LogInformation(L("SuccessDisabled2FA", user.Id));

            return RedirectToAction(nameof(TwoFactorAuthentication));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ResetAuthenticator")]
        public async Task<IActionResult> ResetAuthenticator()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            await _userManager.SetTwoFactorEnabledAsync(user, false);
            await _userManager.ResetAuthenticatorKeyAsync(user);
            _logger.LogInformation(L("SuccessResetAuthenticationKey", user.Id));

            return RedirectToAction(nameof(EnableAuthenticator));
        }

        [HttpGet]
        [Route("ResetAuthenticatorWarning")]
        public IActionResult ResetAuthenticatorWarning()
        {
            return View(nameof(ResetAuthenticator));
        }

        [HttpGet]
        [Route("EnableAuthenticator")]
        public async Task<IActionResult> EnableAuthenticator()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            var model = new EnableAuthenticatorViewModel();
            await LoadSharedKeyAndQrCodeUriAsync(user, model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("EnableAuthenticator")]
        public async Task<IActionResult> EnableAuthenticator(EnableAuthenticatorViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            if (!ModelState.IsValid)
            {
                await LoadSharedKeyAndQrCodeUriAsync(user, model);
                return View(model);
            }

            var verificationCode = model.Code.Replace(" ", string.Empty).Replace("-", string.Empty);

            var is2faTokenValid = await _userManager.VerifyTwoFactorTokenAsync(
                user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

            if (!is2faTokenValid)
            {
                ModelState.AddModelError(L("ErrorCode"), L("InvalidVerificationCode"));
                await LoadSharedKeyAndQrCodeUriAsync(user, model);
                return View(model);
            }

            await _userManager.SetTwoFactorEnabledAsync(user, true);
            var userId = await _userManager.GetUserIdAsync(user);

            _logger.LogInformation(L("SuccessUserEnabled2FA"), userId);

            StatusMessage = L("AuthenticatorVerified");

            if (await _userManager.CountRecoveryCodesAsync(user) == 0)
            {
                var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
                TempData[RecoveryCodesKey] = recoveryCodes.ToArray();

                return RedirectToAction(nameof(ShowRecoveryCodes));
            }

            return RedirectToAction(nameof(TwoFactorAuthentication));
        }

        [HttpGet]
        [Route("GenerateRecoveryCodesWarning")]
        public async Task<IActionResult> GenerateRecoveryCodesWarning()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(L("UserNotFound", _userManager.GetUserId(User)));
            }

            if (!user.TwoFactorEnabled)
            {
                throw new ApplicationException(L("Error2FANotEnabled", user.Id));
            }

            return View(nameof(GenerateRecoveryCodes));
        }

        private async Task LoadSharedKeyAndQrCodeUriAsync(TUser user, EnableAuthenticatorViewModel model)
        {
            var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(unformattedKey))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            model.SharedKey = FormatKey(unformattedKey);
            model.AuthenticatorUri = GenerateQrCodeUri(user.Email, unformattedKey);
        }

        private async Task<IndexViewModel> BuildManageIndexViewModelAsync(TUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var profile = OpenIdClaimHelpers.ExtractProfileInfo(claims);

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage,
                Name = profile.FullName,
                Website = profile.Website,
                Profile = profile.Profile,
                Country = profile.Country,
                Region = profile.Region,
                PostalCode = profile.PostalCode,
                Locality = profile.Locality,
                StreetAddress = profile.StreetAddress
            };
            return model;
        }

        private async Task UpdateUserClaimsAsync(IndexViewModel model, TUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var oldProfile = OpenIdClaimHelpers.ExtractProfileInfo(claims);
            var newProfile = new OpenIdProfile
            {
                Website = model.Website,
                StreetAddress = model.StreetAddress,
                Locality = model.Locality,
                PostalCode = model.PostalCode,
                Region = model.Region,
                Country = model.Country,
                FullName = model.Name,
                Profile = model.Profile
            };

            var claimsToRemove = OpenIdClaimHelpers.ExtractClaimsToRemove(oldProfile, newProfile);
            var claimsToAdd = OpenIdClaimHelpers.ExtractClaimsToAdd(oldProfile, newProfile);
            var claimsToReplace = OpenIdClaimHelpers.ExtractClaimsToReplace(claims, newProfile);

            await _userManager.RemoveClaimsAsync(user, claimsToRemove);
            await _userManager.AddClaimsAsync(user, claimsToAdd);

            foreach (var pair in claimsToReplace)
            {
                await _userManager.ReplaceClaimAsync(user, pair.Item1, pair.Item2);
            }
        }

        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            var currentPosition = 0;

            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }

            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        private string GenerateQrCodeUri(string email, string unformattedKey)
        {
            return string.Format(
                AuthenticatorUriFormat,
                _urlEncoder.Encode("Hybrid.IdentityServer4.STS.Identity"),
                _urlEncoder.Encode(email),
                unformattedKey);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private void AddError(string description, string title = "")
        {
            ModelState.AddModelError(title, description);
        }
    }
}
