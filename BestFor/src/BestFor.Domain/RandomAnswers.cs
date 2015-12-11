using BestFor.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BestFor.Domain
{
    /// <summary>
    /// Implements the default storage of word phrase suggestions
    /// </summary>
    public class RandomAnswers : IAnswerDataSource
    {
        private class AnswerProperties
        {
            public int Count { get; set; }

            public ObjectsIdentifier Id { get; set; }
        }
        #region Some Data
        private static Dictionary<string, Dictionary<string, AnswerProperties>> _data = new Dictionary<string, Dictionary<string, AnswerProperties>>();
        private static bool _dictionaryInitialized = false;
        #endregion

        public RandomAnswers()
        {
            if (!_dictionaryInitialized)
            {
                _data.Add(Answer.FormKey("abc", "def"), new Dictionary<string, AnswerProperties>
                {
                    ["test1"] = new AnswerProperties() { Count = 5, Id = new ObjectsIdentifier() },
                    ["test2"] = new AnswerProperties() { Count = 7, Id = new ObjectsIdentifier() },
                    ["test3"] = new AnswerProperties() { Count = 1, Id = new ObjectsIdentifier() }
                });
                _data.Add(Answer.FormKey("qwe", "qwe"), new Dictionary<string, AnswerProperties>
                {
                    ["test1"] = new AnswerProperties() { Count = 3, Id = new ObjectsIdentifier() },
                    ["test2"] = new AnswerProperties() { Count = 57, Id = new ObjectsIdentifier() },
                    ["test3"] = new AnswerProperties() { Count = 2, Id = new ObjectsIdentifier() }
                });
                _data.Add(Answer.FormKey("asd", "asd"), new Dictionary<string, AnswerProperties>
                {
                    ["test1"] = new AnswerProperties() { Count = 52, Id = new ObjectsIdentifier() },
                    ["test2"] = new AnswerProperties() { Count = 73, Id = new ObjectsIdentifier() },
                    ["test3"] = new AnswerProperties() { Count = 41, Id = new ObjectsIdentifier() }
                });
            }
        }

        /// <summary>
        /// Implmentation of suggestion search based on word start
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IEnumerable<Answer> FindAnswers(string leftWord, string rightWord)
        {
            // find the key
            var key = Answer.FormKey(leftWord, rightWord);
            if (!_data.ContainsKey(key)) return Enumerable.Empty<Answer>();

            var answers = _data[key];

            return answers.Take(10).Select(x => new Answer() { Phrase = x.Key, Id = x.Value.Id, Count = x.Value.Count });
        }

        public Answer AddAnswer(Answer answer)
        {
            var key = answer.Key;
            if (_data.ContainsKey(key))
                AddAnswerInternal(_data[key], answer);
            else
            {
                var answers = new Dictionary<string, AnswerProperties>();
                _data.Add(key, answers);
                AddAnswerInternal(answers, answer);
            }
            return answer;
        }

        private void AddAnswerInternal(Dictionary<string, AnswerProperties> answers, Answer answer)
        {
            AnswerProperties localAnswer;

            if (answers.ContainsKey(answer.Phrase))
            {
                answer.Count = answers[answer.Phrase].Count++;
                answer.Id = answers[answer.Phrase].Id;
            }
            else
            {
                localAnswer = new AnswerProperties() { Id = answer.Id, Count = 1 };
                answers.Add(answer.Phrase, localAnswer);
            }
        }
    }
}
