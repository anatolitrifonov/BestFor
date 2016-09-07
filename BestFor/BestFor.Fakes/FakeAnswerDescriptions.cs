using BestFor.Domain.Entities;

namespace BestFor.Fakes
{
    /// <summary>
    /// Implements a fake dbset of known answers. Used in unit tests.
    /// </summary>
    public class FakeAnswerDescriptions : FakeDbSet<AnswerDescription>
    {
        public FakeAnswerDescriptions() : base()
        {
            Add(new AnswerDescription { Id = 0, Description = "A", UserId ="A", AnswerId = 1 });
            Add(new AnswerDescription { Id = 1, Description = "B", UserId = null, AnswerId = 2 });
            Add(new AnswerDescription { Id = 3, Description = "C", UserId = null, AnswerId = 3 });
        }
    }
}
