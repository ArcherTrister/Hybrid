using Hybrid.Dependency;
using Hybrid.Extensions;

using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Hybrid.Net.Mail.Smtp
{
    /// <summary>
    /// Used to send emails over SMTP.
    /// </summary>
    public class SmtpEmailSender : EmailSenderBase, ISmtpEmailSender, ITransientDependency
    {
        private readonly IServiceProvider _provider;

        /// <summary>
        /// Creates a new <see cref="SmtpEmailSender"/>.
        /// </summary>
        /// <param name="provider">Configuration</param>
        public SmtpEmailSender(IServiceProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        public SmtpClient BuildClient()
        {
            var host = MailSenderOptions.Host;
            var port = MailSenderOptions.Port;

            var smtpClient = new SmtpClient(host, port);
            try
            {
                if (MailSenderOptions.EnableSsl)
                {
                    smtpClient.EnableSsl = true;
                }

                if (MailSenderOptions.UseDefaultCredentials)
                {
                    smtpClient.UseDefaultCredentials = true;
                }
                else
                {
                    smtpClient.UseDefaultCredentials = false;

                    var userName = MailSenderOptions.UserName;
                    if (!userName.IsNullOrEmpty())
                    {
                        var password = MailSenderOptions.Password;
                        var domain = MailSenderOptions.Domain;
                        smtpClient.Credentials = !domain.IsNullOrEmpty()
                            ? new NetworkCredential(userName, password, domain)
                            : new NetworkCredential(userName, password);
                    }
                }

                return smtpClient;
            }
            catch
            {
                smtpClient.Dispose();
                throw;
            }
        }

        protected override async Task SendEmailAsync(MailMessage mail)
        {
            using (var smtpClient = BuildClient())
            {
                await smtpClient.SendMailAsync(mail);
            }
        }

        protected override void SendEmail(MailMessage mail)
        {
            using (var smtpClient = BuildClient())
            {
                smtpClient.Send(mail);
            }
        }
    }
}