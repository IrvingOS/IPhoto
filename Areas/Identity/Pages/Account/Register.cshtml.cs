using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using IPhoto.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace IPhoto.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = string.Format("https://photo.isopen.top/Identity/Account/ConfirmEmail?userId={0}&code={1}&returnUrl={2}", user.Id, code, returnUrl);
                    await _emailSender.SendEmailAsync(Input.Email, "邮箱验证", $"<head><base target='_blank'/><style type='text/css'>::-webkit-scrollbar{{display: none;}}</style><style id='cloudAttachStyle' type='text/css'>#divNeteaseBigAttach, #divNeteaseBigAttach_bak{{display:none;}}</style><style id='blockquoteStyle' type='text/css'>blockquote{{display: none;}}</style><style type = 'text/css'>body{{font-size:14px; font-family:arial,verdana,sans-serif; line-height:1.666; padding: 0; margin: 0; overflow: auto; white-space:normal; word-wrap:break-word; min-height:100px}}td, input, button, select, body{{font-family:Helvetica, 'Microsoft Yahei', verdana}}pre {{white-space:pre-wrap; white-space:-moz-pre-wrap; white-space:-pre-wrap; white-space:-o-pre-wrap; word-wrap:break-word; width: 95%}}th,td{{font-family:arial,verdana,sans-serif; line-height:1.666}}img{{border: 0}}header,footer,section,aside,article,nav,hgroup,figure,figcaption{{display: block}}blockquote{{margin-right:0px}}</style></head><body tabindex='0' role='listitem'><table width='700' border='0' align='center' cellspacing='0' style='width:700px;'><tbody><tr><td><div style='width:700px;margin:0 auto;border-bottom:1px solid #ccc;margin-bottom:30px;'><table border = '0' cellpadding = '0' cellspacing = '0' width = '700' height = '39' style = 'font:12px Tahoma, Arial, 宋体;'><tbody><tr><td width = '210'></td></tr></tbody></table></div><div style = 'width:680px;padding:0 10px;margin:0 auto;'><div style = 'line-height:1.5;font-size:14px;margin-bottom:25px;color:#4d4d4d;'><strong style = 'display:block;margin-bottom:15px;'> 尊敬的用户：您好！</strong><strong style = 'display:block;margin-bottom:15px;'>您正在进行 <a href='https://photo.isopen.top'>IPhoto</a> <span style = 'color: red'> 邮箱验证 </span> 操作。点击<a href='{callbackUrl}'>链接</a>，以完成操作。</strong></div><div style = 'margin-bottom:30px;'><small style = 'display:block;margin-bottom:20px;font-size:12px;'><p style = 'color:#747474;'>注意：此操作可能会修改您的账号、密码或登录邮箱。如非本人操作，请及时登录并修改密码以保证帐户安全。<br>（工作人员不会向你索取此验证码，请勿泄漏！)</p></small></div></div><div style = 'width:700px;margin:0 auto;'><div style = 'padding:10px 10px 0;border-top:1px solid #ccc;color:#747474;margin-bottom:20px;line-height:1.3em;font-size:12px;'><p> 此邮箱为 IPhoto 官方邮箱，期待您的来信<br>此为系统邮件，无需回复<br>请保管好您的邮箱，避免账号被他人盗用</p><p> Irvingsoft </p></div></div></td></tr></tbody></table></body>");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
