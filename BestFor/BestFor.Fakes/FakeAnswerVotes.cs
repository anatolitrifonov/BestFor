using BestFor.Domain.Entities;

namespace BestFor.Fakes
{
    /// <summary>
    /// Implements a fake dbset of known answers. Used in unit tests.
    /// </summary>
    public class FakeAnswerVotes : FakeDbSet<AnswerVote>
    {
        public FakeAnswerVotes() : base()
        {
            Add(new AnswerVote { Id = 1, AnswerId = 1, UserId = "A" });
            Add(new AnswerVote { Id = 2, AnswerId = 1, UserId = "A" });
            Add(new AnswerVote { Id = 3, AnswerId = 1, UserId = "A" });
            Add(new AnswerVote { Id = 4, AnswerId = 1, UserId = "A" });
            Add(new AnswerVote { Id = 5, AnswerId = 1, UserId = "A" });
            Add(new AnswerVote { Id = 6, AnswerId = 1, UserId = "A" });

            Add(new AnswerVote { Id = 11, AnswerId = 1, UserId = "A" });
            Add(new AnswerVote { Id = 12, AnswerId = 1, UserId = "A" });
            Add(new AnswerVote { Id = 13, AnswerId = 1, UserId = "A" });
            Add(new AnswerVote { Id = 14, AnswerId = 1, UserId = "A" });
            Add(new AnswerVote { Id = 15, AnswerId = 1, UserId = "A" });
            Add(new AnswerVote { Id = 16, AnswerId = 1, UserId = "A" });
            Add(new AnswerVote { Id = 17, AnswerId = 1, UserId = "A" });
            Add(new AnswerVote { Id = 18, AnswerId = 1, UserId = "A" });
            Add(new AnswerVote { Id = 19, AnswerId = 1, UserId = "A" });
            Add(new AnswerVote { Id = 20, AnswerId = 1, UserId = "A" });
            Add(new AnswerVote { Id = 21, AnswerId = 1, UserId = "A" });
            Add(new AnswerVote { Id = 22, AnswerId = 1, UserId = "A" });
            Add(new AnswerVote { Id = 23, AnswerId = 1, UserId = "A" });
        }

        public int NumberOfTestSuggestions = 6;
        public int NumberOfAbcSuggestions = 13;
    }
}
