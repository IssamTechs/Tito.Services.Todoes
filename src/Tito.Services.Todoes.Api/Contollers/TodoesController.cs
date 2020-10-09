using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tito.Services.Todoes.Application.Commands;
using Tito.Services.Todoes.Application.DTO;
using Tito.Services.Todoes.Application.Queries;
using Tito.Services.Todoes.Application.Services;

namespace Tito.Services.Todoes.Api.Contollers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoesController : Controller
    {
        private readonly TodoService _todoService;

        public TodoesController(TodoService todoService)
        {
            _todoService = todoService;
        }
       [HttpGet]
       public async Task<ActionResult<IEnumerable<TodoDto>>> GetAll([FromQuery] SearchTodo query)
       {
            return Ok(await _todoService.SearchAsync(query));
       }

       [HttpGet("{todoId:guid}")]
       public async Task<ActionResult<TodoDto>> Get(Guid todoId)
       {
            return Ok(await _todoService.GetAsync(todoId));
       }

       [HttpPost] 
       public async Task<ActionResult> Post(AddTodo command)
        {
            await _todoService.AddAsync(command);
            return Created("api/todoes", command.Id);
           // return CreatedAtAction(nameof(Get), new { todoId = command.Id });
        }

        [HttpPut]
        public async Task<ActionResult> Put(UpdateTodo command)
        {
            await _todoService.UpdateAsync(command);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(DeleteTodo command)
        {
            await _todoService.DeleteAsync(command);
            return Ok();
        }
    }
}
