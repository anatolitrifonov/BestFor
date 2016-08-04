using Microsoft.Extensions.Options;
using BestFor.Common;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace BestFor.Services.Messaging
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private string _emailServerAddress;
        private int _emailServerPort;
        private string _emailServerUser;
        private string _emailServerPassword;
        private string _emailFromAddress;

        public AuthMessageSender(IOptions<AppSettings> appSettings)
        {
            _emailServerAddress = appSettings.Value.EmailServerAddress;
            _emailServerPort = appSettings.Value.EmailServerPort;
            _emailServerUser = appSettings.Value.EmailServerUser;
            _emailServerPassword = appSettings.Value.EmailServerPassword;
            _emailFromAddress = appSettings.Value.EmailFromAddress;
        }

        /// <summary>
        /// Send message to someone
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using (var smtp = new SmtpClient(_emailServerAddress, _emailServerPort))
            {
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(_emailServerUser, _emailServerPassword);
                var mail = new MailMessage
                {
                    Subject = subject,
                    From = new MailAddress(_emailFromAddress),
                    Body = message
                };

                mail.To.Add(email);
                await smtp.SendMailAsync(mail);
            }
        }

        /// <summary>
        /// Send message to ourselves
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(string subject, string message)
        {
            using (var smtp = new SmtpClient(_emailServerAddress, _emailServerPort))
            {
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(_emailServerUser, _emailServerPassword);
                var mail = new MailMessage
                {
                    Subject = subject,
                    From = new MailAddress(_emailFromAddress),
                    Body = message
                };

                mail.To.Add(_emailFromAddress);
                await smtp.SendMailAsync(mail);
            }
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
