using BestFor.Services.Profanity;
using System.Threading.Tasks;

namespace BestFor.Services.Services
{
    public interface IProfanityService
    {
        Task<ProfanityCheckResult> CheckProfanity(string input);
    }
}
