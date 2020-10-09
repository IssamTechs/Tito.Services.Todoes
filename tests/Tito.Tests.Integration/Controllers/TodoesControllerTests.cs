using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Shouldly;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tito.Services.Todoes.Api;
using Tito.Services.Todoes.Core.Entities;
using Tito.Services.Todoes.Core.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Xunit;
using Tito.Services.Todoes.Core.Repositories;
using System.Linq;
using System.Collections.Generic;
using Tito.Services.Todoes.Application.Commands;
using Tito.Services.Todoes.Application.DTO;
using Newtonsoft.Json;

namespace Tito.Tests.Integration.Controllers
{
    public class TodoesControllerTests: IClassFixture<WebApplicationFactory<Startup>>
    {

        private readonly HttpClient _client;
        private readonly ITodoRepository _todoRepository;
        public TodoesControllerTests(WebApplicationFactory<Startup> factory)
        {
            _todoRepository = new TodoRepository();
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services => { services.AddSingleton(_todoRepository); });
            
            }).CreateClient();
        }

        [Fact]
        public async Task get_todo_should_return_dto()
        {
            // act
            var todo = new Todo(Guid.NewGuid(), "Some Title", "Some Description", Priority.HIGH, State.NEW, DateTimeOffset.UtcNow);
            await _todoRepository.AddAsync(todo);
           
            // arrange
            var response = await _client.GetAsync($"/api/todoes/{todo.Id}");
            
            // assert

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task add_todo_should_return_created()
        {
            // act
            var command = new AddTodo (Guid.NewGuid(), "Some Title", "Some Description", "HIGH");
            
            var todo = new Todo(Guid.NewGuid(), "Some Title", "Some Description", Priority.HIGH, State.NEW, DateTimeOffset.UtcNow);

            await _todoRepository.AddAsync(todo);

            var content = new StringContent(JsonConvert.SerializeObject(command), System.Text.Encoding.UTF8, "application/json");
            // arrange
            var response = await _client.PostAsync($"/api/todoes", content);

            // assert
            
            response.StatusCode.ShouldBe(System.Net.HttpStatusCode.Created);
        }

        private class TodoRepository : ITodoRepository
        {
            private readonly List<Todo> _todoes = new List<Todo>();
            public async Task AddAsync(Todo todo)
            {
                _todoes.Add(todo);
            }

            public async Task DeleteAsync(Todo todo)
            {
            }

            public IQueryable<Todo> GetAllAsync()
            {
                throw new NotImplementedException();
            }

            public async Task<Todo> GetAsync(Guid id) => _todoes.SingleOrDefault(x => x.Id == id);

            public async Task UpdateAsync(Todo todo)
            {
               
            }
        }
    }

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ConfigureTestServices()
        {
            base.ConfigureTestServices();
        }
    }

}
