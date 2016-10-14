using System;
using System.Collections.Generic;
using System.Linq;
using BestFor.Domain.Entities;
using BestFor.Domain.Interfaces;
using BestFor.Data;

namespace BestFor.Services.DataSources
{
    public class KeyDataSource<TEntity> where TEntity : EntityBase, IFirstIndex
    {
        public const int DEFAULT_TOP_COUNT = 10;
        private Dictionary<string, TEntity> _data;
        private bool _initialized;

        /// <summary>
        /// Was this set initialized? It will not load itself if already loaded
        /// </summary>
        public bool IsInitialized { get { return _initialized; } }

        /// <summary>
        /// How many answers in cache
        /// </summary>
        public int Size { get { return _data.Count; } }

        public KeyDataSource()
        {

        }

        /// <summary>
        /// Get all items
        /// </summary>
        public IEnumerable<TEntity> Items { get { return _data.Values.AsEnumerable<TEntity>(); } }

        /// <summary>
        /// Load all entities from repository.
        /// </summary>
        /// <param name="repository"></param>
        /// <returns></returns>
        public int Initialize(IRepository<TEntity> repository, int numberItems)
        {
            if (_initialized) throw new Exception("This index is already initialized.");

            _data = new Dictionary<string, TEntity>();
            // partial init
            if (numberItems > 0)
            {
                var count = repository.Count();
                if (count < numberItems) numberItems = count;
                var enumerator = repository.List().GetEnumerator();
                for (var i = 0; i < numberItems; i++)
                {
                    if (enumerator.MoveNext())
                        Insert(enumerator.Current);
                    else
                        break;
                }
            }
            else
            { 
                foreach (var entity in repository.List())
                    Insert(entity);
            }
            _initialized = true;
            return _data.Count;
        }

        // Get all items for the key
        public IEnumerable<TEntity> Find(string key)
        {
            if (_data == null) return null;

            // Get all items for the key
            return _data.Where(x => x.Key.StartsWith(key)).Select(x => x.Value);
        }

        /// <summary>
        /// Find items where key starts with <paramref name="key"/>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindTopItems(string key)
        {
            if (_data == null) return null;

            // Get all items for the key
            // Splitting for debugging
            int c = _data.Where(x => x.Key.StartsWith(key)).Count();
            return _data.Where(x => x.Key.StartsWith(key)).Select(x => x.Value).Take(DEFAULT_TOP_COUNT);
        }

        public TEntity FindExact(string key)
        {
            if (_data == null) return null;

            if (!_data.ContainsKey(key)) return null;

            return _data[key];
        }

        public TEntity Insert(TEntity entity)
        {
            var key = entity.IndexKey;
            // Add to a set of items under index value
            if (!_data.ContainsKey(key))
            {
                _data.Add(key, entity);
            }
            return entity;
        }
    }
}
