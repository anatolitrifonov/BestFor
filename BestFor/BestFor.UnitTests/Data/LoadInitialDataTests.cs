using BestFor.Data;
using BestFor.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Xunit;

namespace BestFor.UnitTests.Data
{
    // Used for loading initial data
    public class LoadInitialDataTests
    {

        /// <summary>
        /// Load Profanity data
        /// </summary>
        [Trait("Data Tests", "Load Profanity")]
        [Fact]
        public void LoadInitialData_Profanity()
        {
            // Uncomment this to actually run.
            // return;

            string pathToSuggestionFile = Directory.GetCurrentDirectory();
            pathToSuggestionFile = pathToSuggestionFile + "\\InitialData\\ProphanityData\\en.txt";

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
            Assert.True(true);
        }

        /// <summary>
        /// Load Resources data
        /// </summary>
        [Fact]
        public void LoadInitialData_Resources()
        {
            // Uncomment this to actually run.
            // return;

            string pathToResourcesFile = Directory.GetCurrentDirectory() + "\\..\\..\\..\\..\\Data\\InitialData\\ResourcesData\\InitialData.sql";
            FileInfo file = new FileInfo(pathToResourcesFile);
            string script = file.OpenText().ReadToEnd();
            script = script.Replace("GO", "");

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = configuration["Data:ATRIFONO-J28FKConnection:ConnectionString"];
            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand(script, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            Assert.True(true);
        }

        /// <summary>
        /// Load Suggestions data
        /// </summary>
        [Fact]
        public void LoadInitialData_Suggestions()
        {
            // Uncomment this to actually run.
           // return;

            string pathToSuggestionFile = Directory.GetCurrentDirectory();
            pathToSuggestionFile = pathToSuggestionFile + "\\InitialData\\SuggestionsData\\suggestions.txt";

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
        }
    }
}
