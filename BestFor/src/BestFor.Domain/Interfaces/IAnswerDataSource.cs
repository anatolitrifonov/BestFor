using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Domain.Interfaces
{
    public interface IAnswerDataSource
    {
        IEnumerable<Answer> FindAnswers(string leftWord, string rightWord);

        Guid AddAnswer(Answer answer);
    }
}
