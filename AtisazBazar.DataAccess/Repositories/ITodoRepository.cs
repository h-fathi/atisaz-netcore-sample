using System;
using System.Threading.Tasks;

namespace AtisazBazar.DataAccess.Repositories
{
    public interface ITodoRepository
    {    
        Task<Guid> AddAsync(Todo todo);
        Task<Todo?> GetByIdAsync(Guid? id);
        Task DeleteAsync(Guid? id);
    }
}