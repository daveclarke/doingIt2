using doingit2_repo.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace doingit2_repo
{
    public class Collection<T> : ICollection<T> where T : Document
    {
        private readonly IMongoDatabase _mongoDb;

        public Collection([NotNull] IMongoDatabase mongoDb)
        {
            _mongoDb = mongoDb;
        }

        /// <summary>
        /// Insert new document
        /// </summary>
        /// <param name="document"></param>
        /// <returns>_id of inserted document</returns>
        [return: NotNull]
        public virtual async Task<string?> CreateAsync(T document)
        {
            var typeName = typeof(T).Name;
            var collection = _mongoDb.GetCollection<T>(typeName);
            if (collection == null)
            {
                await _mongoDb.CreateCollectionAsync(typeName);
                collection = _mongoDb.GetCollection<T>(typeName);
            }
            await collection.InsertOneAsync(document);
            return document.Id;
        }

        public Task<bool> DeleteAsync(T document)
        {
            throw new NotImplementedException();
        }

        [return: MaybeNull]
        public async Task<T?> ReadOrDefaultAsync(string id)
        {
            T? doc = null;
            var typeName = typeof(T).Name;
            var collection = _mongoDb.GetCollection<T>(typeName);
            if (collection != null)
            {
                var filter = Builders<T>.Filter.Eq("Id", id);
                var cursor = await collection.FindAsync(filter);
                doc = await cursor.FirstOrDefaultAsync();
            }
            return doc;
        }

        public async Task UpdateAsync(T document)
        {
            var typeName = typeof(T).Name;
            var collection = _mongoDb.GetCollection<T>(typeName);
            if (collection != null)
            {
                var filter = Builders<T>.Filter.Eq("Id", document.Id);
                var updated = await collection.FindOneAndReplaceAsync(filter, document, new FindOneAndReplaceOptions<T> { ReturnDocument = ReturnDocument.After });
                if (updated == null) throw new KeyNotFoundException("No document found");
            }
        }

        [return: NotNull]
        public IEnumerable<T> GetAll()
        {
            var collection = _mongoDb.GetCollection<T>(typeof(T).Name);
            return collection.AsQueryable<T>().ToEnumerable<T>();
        }
    }
}
