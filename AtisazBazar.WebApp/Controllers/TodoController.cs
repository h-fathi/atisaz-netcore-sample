using AtisazBazar.DataAccess;
using AtisazBazar.Services;
using AtisazBazar.Services.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace AtisazBazar.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {

        private readonly ILogger<TodoController> _logger;
        private readonly ITodoService _todoService;
        public TodoController(ILogger<TodoController> logger, ITodoService todoService)
        {
            _logger = logger;
            _todoService = todoService;
        }


        [Route("Get/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var model = await _todoService.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddAsync(TodoVM todo)
        {
            var recordId = await _todoService.AddAsync(todo);
            return Ok(recordId);
        }

        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _todoService.DeleteAsync(id);
            return Ok();
        }


    }
}