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
    public class KeyIndexedDataSource<TEntity> where TEntity : IndexableEntity
    {
        private static Dictionary<string, Dictionary<int, TEntity>> _data;
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

            _data = new Dictionary<string, Dictionary<int, TEntity>>();
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

        public TEntity Insert(TEntity entity)
        {
            var key = entity.IndexKey;
            // Add to a set of items under index value
            if (_data.ContainsKey(key))
                _data[key].Add(entity.Id, entity);
            else
            {
                // New index value -> add a new set of items
                var newCollection = new Dictionary<int, TEntity>();
                newCollection.Add(entity.Id, entity);
                _data.Add(entity.IndexKey, newCollection);
            }
            return entity;
        }
    }
}
