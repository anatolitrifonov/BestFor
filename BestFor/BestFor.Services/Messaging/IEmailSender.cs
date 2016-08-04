using System.Threading.Tasks;

namespace BestFor.Services.Messaging
{
    public interface IEmailSender
    {
        // Send email to someone
        Task SendEmailAsync(string email, string subject, string message);

        // Send email to ourself
        Task SendEmailAsync(string subject, string message);
    }

}
