using BestFor.Domain.Entities;

namespace BestFor.Data.Fakes
{
    public class FakeAnswers : FakeDbSet<Answer>
    {
        public FakeAnswers() : base()
        {
            Add(new Answer { LeftWord = "asd", RightWord = "asd", Phrase = "test1" });
            Add(new Answer { LeftWord = "asd", RightWord = "asd", Phrase = "test2" });
            Add(new Answer { LeftWord = "asd", RightWord = "asd", Phrase = "test3" });

            Add(new Answer { LeftWord = "qwe", RightWord = "qwe", Phrase = "test1" });
            Add(new Answer { LeftWord = "qwe", RightWord = "qwe", Phrase = "test2" });
            Add(new Answer { LeftWord = "qwe", RightWord = "qwe", Phrase = "test3" });

            Add(new Answer { LeftWord = "abc", RightWord = "def", Phrase = "test1" });
            Add(new Answer { LeftWord = "abc", RightWord = "def", Phrase = "test2" });
            Add(new Answer { LeftWord = "abc", RightWord = "def", Phrase = "test3" });
        }
    }
}
