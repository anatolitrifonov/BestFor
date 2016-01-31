using System;
using System.Collections.Generic;
using System.Linq;
using BestFor.Domain.Entities;

namespace BestFor.Data
{
    /// <summary>
    /// Implements methods specific to AnswerDescription entity.
    /// </summary>
    /// <remarks>
    /// This class helps not to drag queries logic into domain, services or anywhere else
    /// </remarks>
    public class AnswerDescriptionsRepository
    {
        private IRepository<AnswerDescription> _repository;

        /// <summary>
        /// Easy way to instantiate from generic repository
        /// </summary>
        /// <param name="repository"></param>
        public AnswerDescriptionsRepository(IRepository<AnswerDescription> repository)
        {
            _repository = repository;
        }
    }
}
