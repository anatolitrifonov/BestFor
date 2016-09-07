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
    public class AnswerRepository : Repository<Answer>, IAnswerRepository, IRepository<Answer>
    {
        /// <summary>
        /// Easy way to instantiate from generic repository
        /// </summary>
        /// <param name="repository"></param>
        public AnswerRepository(IDataContext context) : base(context)
        {
        }

        /// <summary>
        /// Returns the answers tending "today"
        /// </summary>
        /// <param name="numberItemsToReturn">Number of items to return.</param>
        /// <param name="today">Specify "today". Used for testing.</param>
        /// <returns></returns>
        public IEnumerable<Answer> FindAnswersTrendingToday(int numberItemsToReturn, DateTime today)
        {
            if (numberItemsToReturn < 1 || numberItemsToReturn > 1000)
                throw new ArgumentOutOfRangeException("numberItemsToReturn", "numberItemsToReturn must be between 1 and 1000");

            return Queryable().Where(x =>
                x.DateAdded.Year == today.Year &&
                x.DateAdded.Month == today.Month &&
                x.DateAdded.Day == today.Day &&
                !x.IsHidden)
                .OrderByDescending(x => x.Count)
                .ThenByDescending(x => x.DateAdded)
                .Take(numberItemsToReturn).AsEnumerable();
        }

        /// <summary>
        /// Returns the answers tending overall
        /// </summary>
        /// <param name="numberItemsToReturn"></param>
        /// <returns></returns>
        public IEnumerable<Answer> FindAnswersTrendingOverall(int numberItemsToReturn)
        {
            if (numberItemsToReturn < 1 || numberItemsToReturn > 1000)
                throw new ArgumentOutOfRangeException("numberItemsToReturn", "numberItemsToReturn must be between 1 and 1000");

            return Queryable().Where(x => !x.IsHidden)
                .OrderByDescending(x => x.Count)
                .ThenByDescending(x => x.DateAdded)
                .Take(numberItemsToReturn).AsEnumerable();
        }

        /// <summary>
        /// Only return non hidden answers.
        /// </summary>
        /// <returns></returns>
        public override IQueryable<Answer> Active()
        {
            return _dbSet.Where(x => !x.IsHidden);
        }


        public IEnumerable<Answer> FindByUserId(string userId)
        {
            return _dbSet.Where(x => x.UserId == userId);
        }

        public IEnumerable<Answer> FindAnswersWithNoUser()
        {
            return _dbSet.Where(x => x.UserId == null);
        }
    }
}
