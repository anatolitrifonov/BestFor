using BestFor.Dto;
using System.Threading.Tasks;

namespace BestFor.Services.Services
{
    public interface IStatusService
    {
        SystemStateDto GetSystemStatus();

        int InitAnswers();

        int InitAnswerDescriptions();

        int InitSuggestions();

        int InitBadWords();

        int InitUsers();
    }
}
