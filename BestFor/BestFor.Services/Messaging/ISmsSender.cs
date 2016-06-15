using System.Threading.Tasks;

namespace BestFor.Services.Messaging
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
