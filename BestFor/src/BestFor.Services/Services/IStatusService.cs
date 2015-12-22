using BestFor.Dto;

namespace BestFor.Services.Services
{
    public interface IStatusService
    {
        SystemStateDto GetSystemStatus();

        int InitAnswers();

        int InitSuggestions();

        int InitBadWords();
    }
}
