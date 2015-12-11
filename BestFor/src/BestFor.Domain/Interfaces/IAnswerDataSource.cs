using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestFor.Domain.Entities;

namespace BestFor.Domain.Interfaces
{
    public interface IAnswerDataSource
    {
        IEnumerable<Answer> FindAnswers(string leftWord, string rightWord);

        Answer AddAnswer(Answer answer);
    }
}
