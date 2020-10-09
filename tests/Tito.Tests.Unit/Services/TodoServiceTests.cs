using NSubstitute;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tito.Services.Todoes.Application;
using Tito.Services.Todoes.Application.Commands;
using Tito.Services.Todoes.Application.DTO;
using Tito.Services.Todoes.Application.Paginations;
using Tito.Services.Todoes.Application.Queries;
using Tito.Services.Todoes.Application.Services;
using Tito.Services.Todoes.Core.Entities;
using Tito.Services.Todoes.Core.Repositories;
using Tito.Services.Todoes.Core.ValueObjects;
using Xunit;

namespace Tito.Tests.Unit.Services
{
    public class TodoServiceTests
    {
        private readonly ITodoService _todosService;
        private readonly ITodoRepository _todoRepository;


        static List<Todo> todoList = new List<Todo>
            {
                new Todo (Guid.NewGuid(), "Title #1", "Desc #1",Priority.HIGH, State.NEW, new DateTimeOffset(2010, 01, 01, 0, 0, 0, new TimeSpan())),
                new Todo (Guid.NewGuid(), "Title #2", "Desc #2",Priority.LOW, State.INPROCESS, new DateTimeOffset(2012, 01, 01, 0, 0, 0, new TimeSpan())),
                new Todo (Guid.NewGuid(), "Title #3", "Desc #3",Priority.MEDIUM, State.NEW, new DateTimeOffset(2012, 01, 01, 0, 0, 0, new TimeSpan())),
                new Todo (Guid.NewGuid(), "Title #4", "Desc #4",Priority.HIGH, State.COMPLETED, new DateTimeOffset(2014, 01, 01, 0, 0, 0, new TimeSpan()))
            };

        public TodoServiceTests()
        {
            _todoRepository = Substitute.For<ITodoRepository>();
            _todosService = new TodoService(_todoRepository);
        }
        


        [Fact]
        public async Task get_async_should_return_todo_dto()
        {
            // Arrange
            var todo = new Todo(Guid.NewGuid(), "Some Title", "Some Description", Priority.HIGH, State.NEW);
            _todoRepository.GetAsync(todo.Id).Returns(todo);
            // Act
            var todoDto = await _todosService.GetAsync(todo.Id);
            // Assert
            todoDto.ShouldNotBe(null);
            todoDto.Id.ShouldBe(todo.Id); 
            await _todoRepository.Received().GetAsync(todo.Id); // check if this metthod has received the following call
        }


        [Fact]
        public async Task add_todo_should_succeed_given_valid_title_and_description_and_priority()
        {
            var command = new AddTodo(Guid.Empty, "Some Title", "Some Description", "HIGH");
            
            await _todosService.AddAsync(command);

            await _todoRepository.Received().AddAsync(Arg.Is<Todo>(x => 
            x.Id == command.Id && 
            x.Title == command.Title && 
            x.Description == command.Description &&
            x.Priority == Priority.HIGH));
        }


        [Fact]
        public async Task update_todo_should_succeed_given_valid_title_and_description_and_priority_and_state()
        {
            var command = new UpdateTodo(Guid.NewGuid(), "Some Title", "Some Description", "HIGH", "INPROCESS");
       
            await _todosService.UpdateAsync(command);

            await _todoRepository.Received().UpdateAsync(Arg.Is<Todo>(x =>
            x.Id == command.Id &&
            x.Title == command.Title &&
            x.Description == command.Description &&
            x.Priority == Priority.HIGH &&
            x.State == State.INPROCESS));
        }

        [Fact]
        public async Task delete_todo_should_succeed_given_valid_id()
        {
            var todo = new Todo(Guid.NewGuid(), "Some Title", "Some Description", Priority.HIGH, State.NEW);
            
            _todoRepository.GetAsync(todo.Id).Returns(todo);

            var command = new DeleteTodo(todo.Id);

            await _todosService.DeleteAsync(command);

            await _todoRepository.Received().DeleteAsync(Arg.Is<Todo>(x =>
               x.Id == todo.Id &&
               x.Title == todo.Title &&
               x.Description == todo.Description &&
               x.Priority == todo.Priority &&
               x.State == todo.State &&
               x.CreatedAt == todo.CreatedAt));
        }

        [Fact]
        public async Task search_todo_should_given_no_filter_should_return_default_paged_result()
        { 
             SearchTodo query = null;

            _todoRepository.GetAllAsync().Returns(todoList.AsQueryable());

            var expected = PagedResult<Todo>.Create(todoList, 1, 10, 1, 4).Map(x=>x.AsDto());

            var actuals =  await _todosService.SearchAsync(query);

            _todoRepository.Received().GetAllAsync();
            expected.Items.Count().ShouldBe(actuals.Items.Count());
        }

        [Fact]
        public async Task search_todo_should_given_filter_should_return_data_based_on_paging_option_requested()
        {
            SearchTodo query = new SearchTodo { Page = 1, PageSize = 2 };

            _todoRepository.GetAllAsync().Returns(todoList.AsQueryable());

            var filtered = todoList.Skip(0).Take(2);

            var expected = PagedResult<Todo>.Create(filtered, 1, 10, 2, 4).Map(x => x.AsDto());

            var actuals = await _todosService.SearchAsync(query);

            _todoRepository.Received().GetAllAsync();

            expected.Items.Count().ShouldBe(actuals.Items.Count());
        }

        [Fact]
        public async Task search_todo_should_given_search_filter_should_return_data_based_on_paging_option_requested()
        {
            SearchTodo query = new SearchTodo { Title = "Title #1"};

            _todoRepository.GetAllAsync().Returns(todoList.AsQueryable());

            var filtered = todoList.Where(x => x.Title.Value.Contains(query.Title));

            var expected = PagedResult<Todo>.Create(filtered, 1, 10, 1, 4).Map(x => x.AsDto());

            var actuals = await _todosService.SearchAsync(query);

            _todoRepository.Received().GetAllAsync();

            expected.Items.Count().ShouldBe(actuals.Items.Count());
        }
    }
}
