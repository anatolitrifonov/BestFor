using System.Linq;
using System.Collections.Generic;
using BestFor.Domain.Entities;

namespace BestFor.Data
{
    /// <summary>
    /// Implements methods specific to AnswerDescription entity.
    /// </summary>
    /// <remarks>
    /// This class helps not to drag queries logic into domain, services or anywhere else
    /// </remarks>
    public class AnswerDescriptionRepository : Repository<AnswerDescription>, IAnswerDescriptionRepository, IRepository<AnswerDescription>
    {
        /// <summary>
        /// Easy way to instantiate from generic repository
        /// </summary>
        /// <param name="repository"></param>
        public AnswerDescriptionRepository(IDataContext context) : base(context)
        {
        }

        public IEnumerable<AnswerDescription> FindAnswerDescriptionsWithNoUser()
        {
            return _dbSet.Where(x => x.UserId == null);
        }

        public IEnumerable<AnswerDescription> FindByAnswerId(int answerId)
        {
            return _dbSet.Where(x => x.AnswerId == answerId);
        }

        public IEnumerable<AnswerDescription> FindByUserId(string userId)
        {
            return _dbSet.Where(x => x.UserId == userId);
        }
    }
}

