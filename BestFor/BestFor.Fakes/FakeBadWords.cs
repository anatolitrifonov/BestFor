using BestFor.Domain.Entities;

namespace BestFor.Fakes
{
    /// <summary>
    /// Implements a fake dbset of known answers. Used in unit tests.
    /// </summary>
    public class FakeBadWords : FakeDbSet<BadWord>
    {
        public FakeBadWords() : base()
        {
            Add(new BadWord { Id = 0, Phrase = "2g1c" });
            Add(new BadWord { Id = 1, Phrase = "ponyplay" });
            Add(new BadWord { Id = 2, Phrase = "pole smoker" });
            Add(new BadWord { Id = 3, Phrase = "pleasure chest" });
            Add(new BadWord { Id = 4, Phrase = "playboy" });
            Add(new BadWord { Id = 5, Phrase = "pisspig" });
            Add(new BadWord { Id = 6, Phrase = "piss pig" });
            Add(new BadWord { Id = 7, Phrase = "pissing" });
            Add(new BadWord { Id = 8, Phrase = "piece of shit" });
            Add(new BadWord { Id = 9, Phrase = "phone sex" });
            Add(new BadWord { Id = 10, Phrase = "penis" });
            Add(new BadWord { Id = 11, Phrase = "pegging" });
            Add(new BadWord { Id = 12, Phrase = "pedophile" });
            Add(new BadWord { Id = 13, Phrase = "pedobear" });
            Add(new BadWord { Id = 14, Phrase = "panty" });
            Add(new BadWord { Id = 15, Phrase = "panties" });
            Add(new BadWord { Id = 16, Phrase = "paki" });
            Add(new BadWord { Id = 17, Phrase = "paedophile" });
            Add(new BadWord { Id = 18, Phrase = "orgy" });
            Add(new BadWord { Id = 19, Phrase = "orgasm" });
            Add(new BadWord { Id = 20, Phrase = "poof" });
            Add(new BadWord { Id = 21, Phrase = "one guy one jar" });
            Add(new BadWord { Id = 22, Phrase = "poon" });
            Add(new BadWord { Id = 23, Phrase = "punany" });
            Add(new BadWord { Id = 24, Phrase = "reverse cowgirl" });
            Add(new BadWord { Id = 25, Phrase = "rectum" });
            Add(new BadWord { Id = 26, Phrase = "rapist" });
            Add(new BadWord { Id = 27, Phrase = "raping" });
            Add(new BadWord { Id = 28, Phrase = "rape" });
            Add(new BadWord { Id = 29, Phrase = "raging boner" });
            Add(new BadWord { Id = 30, Phrase = "raghead" });
        }
    }
}
