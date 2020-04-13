using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace doingit2_repo
{
    interface ICollection<T> where T : class
    {
        Task<string?> CreateAsync(T document);
        Task<T?> ReadOrDefaultAsync(string id);
        Task UpdateAsync(T document);
        Task<bool> DeleteAsync(T document);
        IEnumerable<T> GetAll();
    }
}
