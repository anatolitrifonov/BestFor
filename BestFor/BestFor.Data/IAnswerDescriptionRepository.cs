using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestFor.Domain.Entities;

namespace BestFor.Data
{
    public interface IAnswerDescriptionRepository : IRepository<AnswerDescription>
    {
        IEnumerable<AnswerDescription> FindAnswerDescriptionsWithNoUser();

        IEnumerable<AnswerDescription> FindByAnswerId(int answerId);

        IEnumerable<AnswerDescription> FindByUserId(string userId);
    }
}
