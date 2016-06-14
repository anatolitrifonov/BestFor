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
    }
}
