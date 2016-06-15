using BestFor.Dto;
using System.Threading.Tasks;

namespace BestFor.Services.Services
{
    public interface IVoteService
    {
        Task<int> VoteAnswer(AnswerVoteDto answerVote);

        Task<int> VoteAnswerDescription(AnswerDescriptionVoteDto answerVote);

        Task<int> CountAnswerVotes(int answerId);
    }
}
