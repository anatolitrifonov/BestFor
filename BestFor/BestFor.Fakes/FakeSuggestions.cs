using BestFor.Domain.Entities;

namespace BestFor.Fakes
{
    /// <summary>
    /// Implements a fake dbset of known answers. Used in unit tests.
    /// </summary>
    public class FakeSuggestions : FakeDbSet<Suggestion>
    {
        public FakeSuggestions() : base()
        {
            Add(new Suggestion { Id = 1, Phrase = "test2" });
            Add(new Suggestion { Id = 2, Phrase = "test3" });
            Add(new Suggestion { Id = 3, Phrase = "test4" });
            Add(new Suggestion { Id = 4, Phrase = "test5" });
            Add(new Suggestion { Id = 5, Phrase = "test6" });
            Add(new Suggestion { Id = 6, Phrase = "test7" });

            Add(new Suggestion { Id = 11, Phrase = "abc11" });
            Add(new Suggestion { Id = 12, Phrase = "abc12" });
            Add(new Suggestion { Id = 13, Phrase = "abc13" });
            Add(new Suggestion { Id = 14, Phrase = "abc14" });
            Add(new Suggestion { Id = 15, Phrase = "abc15" });
            Add(new Suggestion { Id = 16, Phrase = "abc16" });
            Add(new Suggestion { Id = 17, Phrase = "abc17" });
            Add(new Suggestion { Id = 18, Phrase = "abc18" });
            Add(new Suggestion { Id = 19, Phrase = "abc19" });
            Add(new Suggestion { Id = 20, Phrase = "abc20" });
            Add(new Suggestion { Id = 21, Phrase = "abc21" });
            Add(new Suggestion { Id = 22, Phrase = "abc22" });
            Add(new Suggestion { Id = 23, Phrase = "abc23" });
        }

        public int NumberOfTestSuggestions = 6;
        public int NumberOfAbcSuggestions = 13;
    }
}
