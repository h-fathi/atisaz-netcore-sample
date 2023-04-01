using AtisazBazar.DataAccess;
using AtisazBazar.DataAccess.Repositories;
using AtisazBazar.Services.DataTransferObjects;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AtisazBazar.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;
        public TodoService(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }
        public async Task<Guid> AddAsync(TodoVM todo)
        {
            var entity = _mapper.Map<Todo>(todo);
            return await _todoRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _todoRepository.DeleteAsync(id);
        }

        public async Task<TodoVM?> GetByIdAsync(Guid id)
        {
            var entity = await _todoRepository.GetByIdAsync(id);
            return _mapper.Map<TodoVM>(entity);
        }
    }
}