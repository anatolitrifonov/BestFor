using BestFor.Dto;
using System.Threading.Tasks;

namespace BestFor.Services.Services
{
    public interface IStatusService
    {
        SystemStateDto GetSystemStatus();

        Task<int> InitAnswers();
        Task<int> InitAnswerDescriptions();

        int InitSuggestions();

        int InitBadWords();

        int InitUsers();
    }
}
