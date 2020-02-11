using Hybrid.EventBuses;
using Hybrid.Net.Mail;
using Hybrid.Web.Identity.Entity;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServer4.Web.Identity.Events
{
    /// <summary>
    /// 注册事件处理程序
    /// </summary>
    public class RegisterEventHandler : EventHandlerBase<RegisterEventData>
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;

        public RegisterEventHandler(UserManager<User> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public override void Handle(RegisterEventData eventData)
        {
            User user = eventData.User;
            string code = _userManager.GenerateEmailConfirmationTokenAsync(user).GetAwaiter().GetResult();
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            string url = $"{eventData.RequestScheme}://{eventData.RequestHost}/Account/ConfirmEmail?userId={user.Id}&code={code}";
            string body =
                $"亲爱的用户 <strong>{user.NickName}</strong>[{user.UserName}]，您好！<br>"
                + $"欢迎注册，激活邮箱请 <a href=\"{url}\" target=\"_blank\"><strong>点击这里</strong></a><br>"
                + $"如果上面的链接无法点击，您可以复制以下地址，并粘贴到浏览器的地址栏中打开。<br>"
                + $"{url}<br>"
                + $"祝您使用愉快！";
            _emailSender.Send(user.Email, "飞鱼 注册邮箱激活邮件", body);
        }

        public override async Task HandleAsync(RegisterEventData eventData, CancellationToken cancelToken = default)
        {
            User user = eventData.User;
            string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            string url = $"{eventData.RequestScheme}://{eventData.RequestHost}/Account/ConfirmEmail?userId={user.Id}&code={code}";
            string body =
                $"亲爱的用户 <strong>{user.NickName}</strong>[{user.UserName}]，您好！<br>"
                + $"欢迎注册，激活邮箱请 <a href=\"{url}\" target=\"_blank\"><strong>点击这里</strong></a><br>"
                + $"如果上面的链接无法点击，您可以复制以下地址，并粘贴到浏览器的地址栏中打开。<br>"
                + $"{url}<br>"
                + $"祝您使用愉快！";
            await _emailSender.SendAsync(user.Email, "飞鱼 注册邮箱激活邮件", body);
        }
    }
}