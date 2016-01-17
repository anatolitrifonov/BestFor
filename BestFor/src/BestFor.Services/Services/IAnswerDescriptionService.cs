using BestFor.Domain.Entities;
using BestFor.Dto;
using System.Threading.Tasks;

namespace BestFor.Services.Services
{
    public interface IAnswerDescriptionService
    {
        Task<AnswerDescription> AddAnswerDescription(AnswerDescriptionDto answerDescription);
    }
}
