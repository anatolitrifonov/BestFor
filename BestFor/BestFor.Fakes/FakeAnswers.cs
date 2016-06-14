using BestFor.Domain.Entities;

namespace BestFor.Fakes
{
    /// <summary>
    /// Implements a fake dbset of known answers. Used in unit tests.
    /// </summary>
    public class FakeAnswers : FakeDbSet<Answer>
    {
        public FakeAnswers() : base()
        {
            Add(new Answer { Id = 0, LeftWord = "asd", RightWord = "asd", Phrase = "test1" });
            Add(new Answer { Id = 1, LeftWord = "asd", RightWord = "asd", Phrase = "test2" });
            Add(new Answer { Id = 2, LeftWord = "asd", RightWord = "asd", Phrase = "test3" });

            Add(new Answer { Id = 3, LeftWord = "qwe", RightWord = "qwe", Phrase = "test1" });
            Add(new Answer { Id = 4, LeftWord = "qwe", RightWord = "qwe", Phrase = "test2" });
            Add(new Answer { Id = 5, LeftWord = "qwe", RightWord = "qwe", Phrase = "test3" });

            Add(new Answer { Id = 6, LeftWord = "abc", RightWord = "def", Phrase = "test1" });
            Add(new Answer { Id = 7, LeftWord = "abc", RightWord = "def", Phrase = "test2" });
            Add(new Answer { Id = 8, LeftWord = "abc", RightWord = "def", Phrase = "test3" });
        }
    }
}
