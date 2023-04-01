using AtisazBazar.Services.DataTransferObjects;

namespace AtisazBazar.Services
{
    public interface ITodoService
    {
        Task<Guid> AddAsync(TodoVM todo);
        Task<TodoVM?> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}