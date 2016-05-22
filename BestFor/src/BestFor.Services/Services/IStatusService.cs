using BestFor.Dto;
using System.Threading.Tasks;

namespace BestFor.Services.Services
{
    public interface IStatusService
    {
        SystemStateDto GetSystemStatus();

        Task<int> InitAnswers();

        int InitSuggestions();

        int InitBadWords();
    }
}
