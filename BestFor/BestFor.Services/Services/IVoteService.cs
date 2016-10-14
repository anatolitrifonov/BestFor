using BestFor.Dto;
using System.Threading.Tasks;

namespace BestFor.Services.Services
{
    public interface IVoteService
    {
        int VoteAnswer(AnswerVoteDto answerVote);

        int VoteAnswerDescription(AnswerDescriptionVoteDto answerVote);

        int CountAnswerVotes(int answerId);
    }
}
