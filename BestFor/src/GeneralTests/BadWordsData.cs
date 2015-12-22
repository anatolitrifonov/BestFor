using System.Linq;
using System.IO;
using BestFor.Data;
using BestFor.Domain.Entities;

namespace GeneralTests
{
    public class BadWordsData
    {
        public static void Initialize()
        {
            string pathToSuggestionFile = Directory.GetCurrentDirectory();
            pathToSuggestionFile = pathToSuggestionFile + "\\ProphanityData\\en.txt";

            var context = new BestDataContext();
            if (context.BadWords.Any()) return;

            var endOfFile = false;

            using (var sr = new StreamReader(new FileStream(pathToSuggestionFile, FileMode.Open)))
            {
                while (!endOfFile)
                {
                    for (var i = 0; i < 1000; i++)
                    {
                        var line = sr.ReadLine();
                        if (line == null)
                        {
                            endOfFile = true;
                            break;
                        }
                        else
                        {
                            context.BadWords.Add(new BadWord() { Phrase = line });
                        }
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
