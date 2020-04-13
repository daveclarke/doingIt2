using doingit2_repo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace doingit2_repo
{
    public class SingletonCollection<T> : ICollection<T> where T : Document
    {
        public Task<string?> CreateAsync(T item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(T item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T?> ReadOrDefaultAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T item)
        {
            throw new NotImplementedException();
        }
    }
}
