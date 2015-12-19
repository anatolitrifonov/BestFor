using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestFor.Domain.Entities;
using BestFor.Data;

namespace BestFor.Services.DatSources
{
    /// <summary>
    /// Indexes data by index. All elements having the same index value are grouped one item.
    /// The idea is to cache this.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class KeyIndexedDataSource<TEntity> where TEntity : EntityBase, IFirstIndex, ISecondIndex
    {
        private const int DEFAULT_TOP_COUNT = 10;
        private Dictionary<string, Dictionary<string, TEntity>> _data;
        private bool _initialized;

        public bool IsInitialized { get { return _initialized; } }
        public KeyIndexedDataSource()
        {

        }

        /// <summary>
        /// Load all entities from repository.
        /// </summary>
        /// <param name="repository"></param>
        /// <returns></returns>
        public int Initialize(IRepository<TEntity> repository)
        {
            if (_initialized) throw new Exception("This index is already initialized.");

            _data = new Dictionary<string, Dictionary<string, TEntity>>();
            foreach (var entity in repository.List())
                Insert(entity);
            _initialized = true;
            return _data.Count;
        }

        // Get all items for the key
        public IEnumerable<TEntity> Find(string key)
        {
            if (_data == null) return null;

            if (!_data.ContainsKey(key)) return null;

            // Get all items for the key
            return _data[key].Values.AsEnumerable();
        }

        public IEnumerable<TEntity> FindTopAnswers(string key)
        {
            if (_data == null) return null;

            if (!_data.ContainsKey(key)) return null;

            // Get all items for the key
            return _data[key].Values.AsEnumerable().Take(DEFAULT_TOP_COUNT);
        }

        public TEntity FindExact(string key, string secondKey)
        {
            if (_data == null) return null;

            if (!_data.ContainsKey(key)) return null;

            // got the first key
            var firstData = _data[key];

            if (!firstData.ContainsKey(secondKey)) return null;

            // got the second key
            return firstData[secondKey];
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
            return entity;
        }

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
    }
}
