using doingit2_repo.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doingit2_repo
{
    public class SingletonCollection<T> : Collection<T> where T : Document
    {
        public SingletonCollection(IMongoDatabase mongoDb) : base(mongoDb) { }

        [return: NotNull]
        public async override Task<string?> CreateAsync(T item)
        {
            if (GetAll().Any()) throw new InvalidOperationException();
            return await base.CreateAsync(item);
        }
    }
}
