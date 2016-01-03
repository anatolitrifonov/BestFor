using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using Microsoft.Data.Entity.Storage;
using BestFor.Domain.Entities;
using BestFor.Data;

namespace GeneralTests
{
    public class SuggestionsData
    {
        public static void Initialize()
        {
            string pathToSuggestionFile = Directory.GetCurrentDirectory();
            pathToSuggestionFile = pathToSuggestionFile + "\\suggestions.txt";

            var context = new BestDataContext();
            if (context.Suggestions.Any()) return;

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
                            context.Suggestions.Add(new Suggestion() { Phrase = line });
                        }

                    }
                    context.SaveChanges();
                }
            }


            //if (!context.Answers.Any())
            //{
            //    var a1 = context.Answers.Add(new Answer { LeftWord = "asd", RightWord = "asd", Phrase = "test1" }).Entity;
            //    var a2 = context.Answers.Add(new Answer { LeftWord = "asd", RightWord = "asd", Phrase = "test2" }).Entity;
            //    var a3 = context.Answers.Add(new Answer { LeftWord = "asd", RightWord = "asd", Phrase = "test3" }).Entity;

            //    var b1 = context.Answers.Add(new Answer { LeftWord = "qwe", RightWord = "qwe", Phrase = "test1" }).Entity;
            //    var b2 = context.Answers.Add(new Answer { LeftWord = "qwe", RightWord = "qwe", Phrase = "test2" }).Entity;
            //    var b3 = context.Answers.Add(new Answer { LeftWord = "qwe", RightWord = "qwe", Phrase = "test3" }).Entity;

            //    var c1 = context.Answers.Add(new Answer { LeftWord = "abc", RightWord = "def", Phrase = "test1" }).Entity;
            //    var c2 = context.Answers.Add(new Answer { LeftWord = "abc", RightWord = "def", Phrase = "test2" }).Entity;
            //    var c3 = context.Answers.Add(new Answer { LeftWord = "abc", RightWord = "def", Phrase = "test3" }).Entity;

            //    //context.Books.AddRange(
            //    //    new Book()
            //    //    {
            //    //        Title = "Pride and Prejudice",
            //    //        Year = 1813,
            //    //        Author = austen,
            //    //        Price = 9.99M,
            //    //        Genre = "Comedy of manners"
            //    //    },
            //    //    new Book()
            //    //    {
            //    //        Title = "Northanger Abbey",
            //    //        Year = 1817,
            //    //        Author = austen,
            //    //        Price = 12.95M,
            //    //        Genre = "Gothic parody"
            //    //    },
            //    //    new Book()
            //    //    {
            //    //        Title = "David Copperfield",
            //    //        Year = 1850,
            //    //        Author = dickens,
            //    //        Price = 15,
            //    //        Genre = "Bildungsroman"
            //    //    },
            //    //    new Book()
            //    //    {
            //    //        Title = "Don Quixote",
            //    //        Year = 1617,
            //    //        Author = cervantes,
            //    //        Price = 8.95M,
            //    //        Genre = "Picaresque"
            //    //    }
            //    // );
            //    context.SaveChanges();
            //}
        }
    }
}
