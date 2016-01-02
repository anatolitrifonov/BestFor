using BestFor.Services.Profanity;

namespace BestFor.Services.Services
{
    public interface IProfanityService
    {
        ProfanityCheckResult CheckProfanity(string input);
    }
}
