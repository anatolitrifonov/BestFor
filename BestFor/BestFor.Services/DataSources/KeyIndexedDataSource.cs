using BestFor.Data;
using BestFor.Domain.Entities;
using BestFor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Services.DataSources
{
    /// <summary>
    /// Indexes data by index. All elements having the same index value are grouped one item.
    /// The idea is to cache this.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <remarks>
    /// In general this is a double index in memory. 
    /// Entity has Key1 and Key2.
    /// Example: Anser has Key1 = LeftWord+RightWord, Key2 = Phrase
    /// Index is a Dictionary of Dictionaries
    /// Fist Dictionary's key is Key1
    /// Second Dictionary's key is Key2
    /// If we have answers
    /// Id  Left    Right   Phrase
    /// 1   Hello   World   Here
    /// 2   Hello   World   There
    /// 3   Hello   Peace   Zoom
    /// 4   Hello   Peace   Luck
    /// Than the first Dictionary will have
    /// item 1 Hello+World
    /// item 2 Hello+World
    /// There will be two second level dictionaries (for Item 1 and Item 2)
    /// The first will have kyes Here and There and contain answers with ids 1 and 2
    /// The second will have kyes Zoom and Luck and contain answers with ids 3 and 4.
    /// 
    /// Here is the more complex scenario: Answer is added -> added to database -> added to cache -> user is offered to add description
    /// -> we pass answer id to the answer description page -> we want to show the content of the answer
    /// -> ideally we do not want to load it from the database since we have it in cache 
    /// -> But cache is not indexed by id at the moment
    /// I suggest we add another dictionary by id for now. Doubt it is going to be too bad.
    /// EntityBase already has id. It will just point straight to the answer object. We might have to replace objects in the second dictionary
    /// on insert too but should not be a big deal. At some point we will have to add locking on add anyway.
    /// todo: May be turn this into all asynchronous?
    /// </remarks>
    //public class KeyIndexedDataSource<TEntity> where TEntity : EntityBase, IFirstIndex, ISecondIndex
    public class KeyIndexedDataSource<TEntity> where TEntity : IFirstIndex, ISecondIndex, IIdIndex
    {
        public class SimpleCountPair
        {
            public string Key { get; set; }
            public int Count { get; set; }
        }

        private const int DEFAULT_TOP_COUNT = 10;
        /// <summary>
        /// Main index data
        /// todo: change the whole thing to ConcurrentDictionary
        /// </summary>
        private Dictionary<string, Dictionary<string, TEntity>> _data;
        /// <summary>
        /// data indexed by id
        /// </summary>
        private Dictionary<int, TEntity> _iddata;
        /// <summary>
        /// Store this because dealing with bool as an indication of "is initialized" is easier.
        /// Could have gotten away with _data == null
        /// </summary>
        private bool _initialized;

        /// <summary>
        /// Was this set initialized? It will not load itself if already loaded
        /// </summary>
        public bool IsInitialized { get { return _initialized; } }

        /// <summary>
        /// How many first level items in cache
        /// </summary>
        public int Size {  get { return _data.Count; } }

        /// <summary>
        /// Load all entities from repository.
        /// </summary>
        /// <param name="repository"></param>
        /// <returns></returns>
        public int Initialize(IEnumerable<TEntity> data)
        {
            if (_initialized) throw new Exception("This index is already initialized.");

            _data = new Dictionary<string, Dictionary<string, TEntity>>();
            _iddata = new Dictionary<int, TEntity>();
            // var howMany = repository.Active().Count();
//            foreach (var entity in repository.Active())
            foreach (var entity in data) Insert(entity);
            _initialized = true;
            return _data.Count;
        }

        /// <summary>
        /// Get all items for the key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks>For example store all answers for the combination of left key and right key</remarks>
        public IEnumerable<TEntity> Find(string key)
        {
            if (_data == null) return null;

            if (!_data.ContainsKey(key)) return null;

            // Get all items for the key
            return _data[key].Values.AsEnumerable();
        }

        public IEnumerable<TEntity> FindTopItems(string key)
        {
            if (_data == null) return null;

            if (!_data.ContainsKey(key)) return null;

            // Get all items for the key
            return _data[key].Values.AsEnumerable().Take(DEFAULT_TOP_COUNT);
        }

        /// <summary>
        /// Return top first index <paramref name="count"/> keys that contain max number of items.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<List<SimpleCountPair>> FindTopIndexKeys(int count)
        {
            if (count < 1)
                throw new Exception("Invalid parameter value " + count + " passed to KeyIndexedDataSource(count)");
            if (_data == null) return null;

            // We can't get away with returning only keys because
            // If we do not return count here then we need to load the counts in
            // user service and store and sync the counts in database
            // Currently I do not want to store counts in the database
            // because I do not want to sync up the counts in users table with counts in answers table
            // This is symply not a good idea on large numbers.
            // We will just hope to read from cache and from database
            var t = _data.OrderByDescending(x => x.Value.Count()).Take(count)
                .Select(x => new SimpleCountPair() { Key = x.Key, Count = x.Value.Count }).ToList();

            // Get all items for the key
            return await Task.FromResult(t);
        }

        public async Task<TEntity> FindExact(string key, string secondKey)
        {
            // No need to throw exception although might be a good idea to tell whoever is calling this to initialize first.
            if (_data == null) return default(TEntity);

            if (!_data.ContainsKey(key)) return default(TEntity);

            // got the first key
            var firstData = _data[key];

            if (!firstData.ContainsKey(secondKey)) return default(TEntity);

            // got the second key
            return await Task.FromResult(firstData[secondKey]);
        }
        
        /// <summary>
        /// Second index contains full list of items unique by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> FindExactById(int id)
        {
            // No need to throw exception although might be a good idea to tell whoever is calling this to initialize first.
            if (_iddata == null) return default(TEntity);

            if (!_iddata.ContainsKey(id)) return default(TEntity);

            // We got direct pointer to the item.
            return await Task.FromResult(_iddata[id]);

        }

        public TEntity Insert(TEntity entity)
        {
            var key = entity.IndexKey;
            // Add to a set of items under index value
            if (_data.ContainsKey(key))
            {
                InsertSecondaryKey(_data[key], entity);
            }
            else
            {
                // New index value -> add a new set of items
                var newCollection = new Dictionary<string, TEntity>();
                InsertSecondaryKey(newCollection, entity);
                _data.Add(entity.IndexKey, newCollection);
            }

            // I would not expect this exception to ever be thrown
            // If it is thrown then something is wrong in design.
            // AT: 4/5/2016. Disregard the previous note. This happens when user adds (essentially votes) for the same answer
            // entity will come with the proper id and increased count. Skip if already there.
            try
            {
                if (!_iddata.ContainsKey(entity.Id))
                    _iddata.Add(entity.Id, entity);
            }
            catch(Exception ex)
            {
                throw new Exception("Something is wrong in the index by id dictionary.", ex);
            }

            return entity;
        }

        /// <summary>
        /// Deletes entity from index
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> Delete(TEntity entity)
        {
            if (entity == null) return default(TEntity);
            if (!_data.ContainsKey(entity.IndexKey)) return default(TEntity);
            // remove from the second index
            var howManyLeft = RemoveSecondaryKey(_data[entity.IndexKey], entity);
            // remove from the first index
            if (howManyLeft == 0) _data.Remove(entity.IndexKey);
            // Remove from id index
            _iddata.Remove(entity.Id);

            return await Task.FromResult(entity);
        }

        /// <summary>
        /// Return all items indexed by IIdIndex.Id
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> All()
        {
            if (_iddata == null) return null;

            return await Task.FromResult(_iddata.Values.AsEnumerable<TEntity>());
        }

        #region Private Methods
        /// <summary>
        /// index here is actually a small dictionary
        /// </summary>
        /// <param name="index"></param>
        /// <param name="entity"></param>
        /// <remarks>Second index stored all answers for a given key consiting on left word and right word </remarks>
        private void InsertSecondaryKey(Dictionary<string, TEntity> index, TEntity entity)
        {
            // Entity has the second key we only want to add it to index if it is not there
            var secondKey = entity.SecondIndexKey;
            if (!index.ContainsKey(entity.SecondIndexKey))
                // add entry
                index.Add(entity.SecondIndexKey, entity);
            else
                // replace the entry because new entry has an updated count
                index[secondKey] = entity;
        }

        private int RemoveSecondaryKey(Dictionary<string, TEntity> index, TEntity entity)
        {
            // Entity has the second key we only want to add it to index if it is not there
            var secondKey = entity.SecondIndexKey;
            if (!index.ContainsKey(entity.SecondIndexKey)) return 0;
            index.Remove(secondKey);
            return index.Count;
        }
        #endregion
    }
}
