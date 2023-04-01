using AtisazBazar.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AtisazBazar.DataAccess.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TodoRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Guid> AddAsync(Todo todo)
        {
            await _dbContext.Todos.AddAsync(todo);
            await _dbContext.SaveChangesAsync();
            return todo.Id;
        }


        public async Task<Todo?> GetByIdAsync(Guid? id)
        {
            if (id is null)
                throw new ArgumentNullException(nameof(id));

            return await _dbContext.Todos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id.Value);
        }

        public async Task DeleteAsync(Guid? id)
        {
            if (id is null)
                throw new ArgumentNullException(nameof(id));

            var item = await _dbContext.Todos.FirstOrDefaultAsync(x => x.Id == id.Value);

            if (item is not null)
            {
                _dbContext.Todos.Remove(item);
               await  _dbContext.SaveChangesAsync();
            }

        }


    }
}
