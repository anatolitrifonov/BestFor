using BestFor.Data;
using BestFor.Domain.Entities;
using BestFor.Dto;
using BestFor.Services.Cache;
using BestFor.Services.DataSources;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestFor.Services.Services
{
    public class StatusService : IStatusService
    {
        private ICacheManager _cacheManager;
        private IAnswerRepository _answersRepository;
        private IRepository<Suggestion> _suggestionRepository;
        private IRepository<BadWord> _badWordsRepository;
        private UserManager<ApplicationUser> _userManager;
        private IAnswerDescriptionRepository _answersDescriptionRepository;

        public StatusService(ICacheManager cacheManager, IAnswerRepository answersRepository, IRepository<Suggestion> suggestionRepository,
            IRepository<BadWord> badWordsRepository, UserManager<ApplicationUser> userManager, IAnswerDescriptionRepository answersDescriptionRepository)
        {
            _cacheManager = cacheManager;
            _answersRepository = answersRepository;
            _suggestionRepository = suggestionRepository;
            _badWordsRepository = badWordsRepository;
            _userManager = userManager;
            _answersDescriptionRepository = answersDescriptionRepository;
        }

        public SystemStateDto GetSystemStatus()
        {
            var result = new SystemStateDto();

            var answersData = _cacheManager.Get(CacheConstants.CACHE_KEY_ANSWERS_DATA);
            if (answersData == null)
            {
                result.AnswersCacheStatus = "Not loaded";
            }
            else
            {
                result.AnswersCacheStatus = "Loaded";
                result.AnswersCacheNumberItems = ((KeyIndexedDataSource<Answer>)answersData).Size;
            }

            var answerDescriptionsData = _cacheManager.Get(CacheConstants.CACHE_KEY_ANSWER_DESCRIPTIONS_DATA);
            if (answerDescriptionsData == null)
            {
                result.AnswersDescriptionCacheStatus = "Not loaded";
            }
            else
            {
                result.AnswersDescriptionCacheStatus = "Loaded";
                result.AnswersDescriptionCacheNumberItems = ((KeyIndexedDataSource<AnswerDescription>)answerDescriptionsData).Size;
            }

            var suggestionsData = _cacheManager.Get(CacheConstants.CACHE_KEY_SUGGESTIONS_DATA);
            if (suggestionsData == null)
            {
                result.SuggestionsCacheStatus = "Not loaded";
            }
            else
            {
                result.SuggestionsCacheStatus = "Loaded";
                result.SuggestionsCacheNumberItems = ((KeyDataSource<Suggestion>)suggestionsData).Size;
            }

            var badWordsData = _cacheManager.Get(CacheConstants.CACHE_KEY_BADWORDS_DATA);
            if (badWordsData == null)
            {
                result.BadWordsCacheStatus = "Not loaded";
            }
            else
            {
                result.BadWordsCacheStatus = "Loaded";
                result.BadWordsCacheNumberItems = ((KeyDataSource<BadWord>)badWordsData).Size;
            }

            var usersData = _cacheManager.Get(CacheConstants.CACHE_KEY_USERS_DATA);
            if (usersData == null)
            {
                result.UsersCacheStatus = "Not loaded";
            }
            else
            {
                result.UsersCacheStatus = "Loaded";
                result.UsersCacheNumberItems = ((Dictionary<string, ApplicationUser>)usersData).Count;
            }

            return result;
        }

        public int InitAnswers()
        {
            var dataSource = new KeyIndexedDataSource<Answer>();
            dataSource.Initialize(_answersRepository.Active());
            _cacheManager.Add(CacheConstants.CACHE_KEY_ANSWERS_DATA, dataSource);
            return dataSource.Size;
        }

        public int InitAnswerDescriptions()
        {
            var dataSource = new KeyIndexedDataSource<AnswerDescription>();
            dataSource.Initialize(_answersDescriptionRepository.Active());
            _cacheManager.Add(CacheConstants.CACHE_KEY_ANSWER_DESCRIPTIONS_DATA, dataSource);
            return dataSource.Size;
        }

        public int InitSuggestions()
        {
            var dataSource = new KeyDataSource<Suggestion>();
            // Limit to 10k for now.
            dataSource.Initialize(_suggestionRepository, 10000);
            _cacheManager.Add(CacheConstants.CACHE_KEY_SUGGESTIONS_DATA, dataSource);
            return dataSource.Size;
        }

        public int InitBadWords()
        {
            var dataSource = new KeyDataSource<BadWord>();
            dataSource.Initialize(_badWordsRepository, 0);
            _cacheManager.Add(CacheConstants.CACHE_KEY_BADWORDS_DATA, dataSource);
            return dataSource.Size;
        }

        public int InitUsers()
        {
            var dataSource = new Dictionary<string, ApplicationUser>();
            foreach (var user in _userManager.Users)
                dataSource.Add(user.Id, user);
            _cacheManager.Add(CacheConstants.CACHE_KEY_USERS_DATA, dataSource);
            return dataSource.Count;
        }
    }
}
