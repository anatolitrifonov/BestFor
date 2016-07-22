using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestFor.Domain.Entities;

namespace BestFor.Data
{
    public interface IAnswerRepository : IRepository<Answer>
    {
        IEnumerable<Answer> FindAnswersTrendingToday(int numberItemsToReturn, DateTime today);

        IEnumerable<Answer> FindAnswersTrendingOverall(int numberItemsToReturn);
    }
}
