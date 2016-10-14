using BestFor.Domain.Entities;

namespace BestFor.Fakes
{
    /// <summary>
    /// Implements a fake dbset of known answers. Used in unit tests.
    /// </summary>
    public class FakeAnswerDescriptionVotes : FakeDbSet<AnswerDescriptionVote>
    {
        public FakeAnswerDescriptionVotes() : base()
        {
            Add(new AnswerDescriptionVote { Id = 1, AnswerDescriptionId = 1 });
            Add(new AnswerDescriptionVote { Id = 2, AnswerDescriptionId = 1 });
            Add(new AnswerDescriptionVote { Id = 3, AnswerDescriptionId = 1 });
            Add(new AnswerDescriptionVote { Id = 4, AnswerDescriptionId = 1 });
            Add(new AnswerDescriptionVote { Id = 5, AnswerDescriptionId = 1 });
            Add(new AnswerDescriptionVote { Id = 6, AnswerDescriptionId = 1 });

            Add(new AnswerDescriptionVote { Id = 11, AnswerDescriptionId = 1 });
            Add(new AnswerDescriptionVote { Id = 12, AnswerDescriptionId = 1 });
            Add(new AnswerDescriptionVote { Id = 13, AnswerDescriptionId = 1 });
            Add(new AnswerDescriptionVote { Id = 14, AnswerDescriptionId = 1 });
            Add(new AnswerDescriptionVote { Id = 15, AnswerDescriptionId = 1 });
            Add(new AnswerDescriptionVote { Id = 16, AnswerDescriptionId = 1 });
            Add(new AnswerDescriptionVote { Id = 17, AnswerDescriptionId = 1 });
            Add(new AnswerDescriptionVote { Id = 18, AnswerDescriptionId = 1 });
            Add(new AnswerDescriptionVote { Id = 19, AnswerDescriptionId = 1 });
            Add(new AnswerDescriptionVote { Id = 20, AnswerDescriptionId = 1 });
            Add(new AnswerDescriptionVote { Id = 21, AnswerDescriptionId = 1 });
            Add(new AnswerDescriptionVote { Id = 22, AnswerDescriptionId = 1 });
            Add(new AnswerDescriptionVote { Id = 23, AnswerDescriptionId = 1 });
        }

        public int NumberOfTestSuggestions = 6;
        public int NumberOfAbcSuggestions = 13;
    }
}
