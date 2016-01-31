using System;
using System.Linq;
using System.Collections.Generic;
using BestFor.Domain.Entities;

namespace BestFor.Data
{
    /// <summary>
    /// Implements methods specific to Answer entity.
    /// </summary>
    /// <remarks>
    /// This class helps not to drag queries logic into domain, services or anywhere else
    /// </remarks>
    public class AnswersRepository
    {
        private IRepository<Answer> _repository;

        /// <summary>
        /// Easy way to instantiate from generic repository
        /// </summary>
        /// <param name="repository"></param>
        public AnswersRepository(IRepository<Answer> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Returns the answers tending "today"
        /// </summary>
        /// <param name="numberItemsToReturn">Number of items to return.</param>
        /// <param name="today">Specify "today". Used for testing.</param>
        /// <returns></returns>
        public IEnumerable<Answer> FindAnswersTrendingToday(int numberItemsToReturn, DateTime today)
        {
            if (numberItemsToReturn < 1 && numberItemsToReturn > 1000)
                throw new ArgumentOutOfRangeException("numberItemsToReturn", "numberItemsToReturn must be between 1 and 1000");

            return _repository.Queryable().Where(x =>
                x.DateAdded.Year == today.Year &&
                x.DateAdded.Month == today.Month &&
                x.DateAdded.Day == today.Day)
                .OrderByDescending(x => x.Count)
                .OrderByDescending(x => x.DateAdded)
                .Take(numberItemsToReturn).AsEnumerable();
        }

        /// <summary>
        /// Returns the answers tending overall
        /// </summary>
        /// <param name="numberItemsToReturn"></param>
        /// <returns></returns>
        public IEnumerable<Answer> FindAnswersTrendingOverall(int numberItemsToReturn)
        {
            if (numberItemsToReturn < 1 && numberItemsToReturn > 1000)
                throw new ArgumentOutOfRangeException("numberItemsToReturn", "numberItemsToReturn must be between 1 and 1000");

            return _repository.Queryable().OrderByDescending(x => x.Count)
                .OrderByDescending(x => x.DateAdded)
                .Take(numberItemsToReturn).AsEnumerable();
        }
    }
}
