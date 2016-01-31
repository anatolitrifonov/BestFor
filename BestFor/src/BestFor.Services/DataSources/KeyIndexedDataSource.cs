﻿using System;
using System.Collections.Generic;
using System.Linq;
using BestFor.Domain.Entities;
using BestFor.Data;

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
    /// </remarks>
    public class KeyIndexedDataSource<TEntity> where TEntity : EntityBase, IFirstIndex, ISecondIndex
    {
        private const int DEFAULT_TOP_COUNT = 10;
        /// <summary>
        /// Main index data
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
        public int Initialize(IRepository<TEntity> repository)
        {
            if (_initialized) throw new Exception("This index is already initialized.");

            _data = new Dictionary<string, Dictionary<string, TEntity>>();
            _iddata = new Dictionary<int, TEntity>();
            foreach (var entity in repository.List())
                Insert(entity);
            _initialized = true;
            return _data.Count;
        }

        /// <summary>
        /// Get all items for the key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
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

        public TEntity FindExact(string key, string secondKey)
        {
            // No need to throw exception although might be a good idea to tell whoever is calling this to initialize first.
            if (_data == null) return null;

            if (!_data.ContainsKey(key)) return null;

            // got the first key
            var firstData = _data[key];

            if (!firstData.ContainsKey(secondKey)) return null;

            // got the second key
            return firstData[secondKey];
        }

        public TEntity FindExactById(int id)
        {
            // No need to throw exception although might be a good idea to tell whoever is calling this to initialize first.
            if (_iddata == null) return null;

            if (!_iddata.ContainsKey(id)) return null;

            // We got direct pointer to the item.
            return _iddata[id];

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
            try
            {
                _iddata.Add(entity.Id, entity);
            }
            catch(Exception ex)
            {
                throw new Exception("Something is wrong in the index by id dictionary.", ex);
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
