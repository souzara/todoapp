using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.AppServices.Dtos;
using ToDoApp.AppServices.Interfaces;
using ToDoApp.Validators;
using Xunit;

namespace ToDoApp.Tests.Api
{
    public class TodoControllerTests : UnitTestsBase
    {

        private Controllers.TodoController controller;

        public TodoControllerTests()
        {
            OverrideRegistration(x => Substitute.For<ITodoAppService>());


            controller = new Controllers.TodoController(InstanceOf<ITodoAppService>(), InstanceOf<TodoValidator>());
        }

        [Fact]
        public void ListDataWithSuccess()
        {
            var todoAppService = InstanceOf<ITodoAppService>();
            todoAppService.List(Arg.Any<TodoFilterDto>()).Returns(new TodoDto[]
            {
                new TodoDto
                {
                    Id = 1,
                    IsCompleted = false,
                    Text = "My mock todo",
                    TodoLogs = Enumerable.Empty<TodoLogDto>()
                }
            });

            var result = controller.Get(new TodoFilterDto());


            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Result);
            Assert.Equal(1, result.Result.Count());
            var todo = result.Result.First();
            Assert.Equal(1, todo.Id);
            Assert.False(todo.IsCompleted);
            Assert.Equal("My mock todo", todo.Text);
            Assert.NotNull(todo.TodoLogs);
            Assert.Equal(0, todo.TodoLogs.Count());

        }

        [Fact]
        public void GetDataByIdWithSuccess()
        {
            var todoAppService = InstanceOf<ITodoAppService>();
            todoAppService.GetById(Arg.Is(1)).Returns(new TodoDto
            {
                Id = 1,
                IsCompleted = false,
                Text = "My mock todo",
                TodoLogs = Enumerable.Empty<TodoLogDto>()
            });

            var result = controller.Get(1);
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Result);
            var todo = result.Result;
            Assert.Equal(1, todo.Id);
            Assert.False(todo.IsCompleted);
            Assert.Equal("My mock todo", todo.Text);
            Assert.NotNull(todo.TodoLogs);
            Assert.Equal(0, todo.TodoLogs.Count());
        }

        [Fact]
        public void GetDataByIdWithoutSuccess()
        {
            var todoAppService = InstanceOf<ITodoAppService>();
            todoAppService.GetById(Arg.Is(1)).Returns((callInfo) =>
            {
                throw new Exception();
            });

            var result = controller.Get(1);
            Assert.NotNull(result);
            Assert.False(result.Success);
        }

        [Fact]
        public void InsertWithSuccess()
        {
            var todoAppService = InstanceOf<ITodoAppService>();

            var todo = new TodoDto
            {
                Id = 1,
                IsCompleted = false,
                Text = "My mock todo",
                TodoLogs = Enumerable.Empty<TodoLogDto>()
            };

            todoAppService.Create(Arg.Any<TodoDto>()).Returns(todo);

            var result = controller.Post(todo);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Result);

        }

        [Fact]
        public void UpdateWithSuccess()
        {
            var todoAppService = InstanceOf<ITodoAppService>();

            var todo = new TodoDto
            {
                Id = 1,
                IsCompleted = false,
                Text = "My mock todo",
                TodoLogs = Enumerable.Empty<TodoLogDto>()
            };

            todoAppService.Update(Arg.Any<TodoDto>()).Returns(true);

            var result = controller.Put(1, todo);

            Assert.NotNull(result);
            Assert.True(result.Success);
        }
        [Fact]
        public void UpdateWithoutSuccessByException()
        {
            var todoAppService = InstanceOf<ITodoAppService>();
            todoAppService.Update(Arg.Any<TodoDto>()).Returns((callInfo) =>
            {
                throw new Exception();
            });
            var todo = new TodoDto
            {
                Id = 1,
                IsCompleted = false,
                Text = "My mock todo",
                TodoLogs = Enumerable.Empty<TodoLogDto>()
            };

            var result = controller.Put(1, todo);

            Assert.NotNull(result);
            Assert.False(result.Success);

        }

        [Fact]
        public void UpdateWithoutSuccess()
        {
            var todoAppService = InstanceOf<ITodoAppService>();
            todoAppService.Update(Arg.Any<TodoDto>()).Returns(false);
            var todo = new TodoDto
            {
                Id = 1,
                IsCompleted = false,
                Text = "My mock todo",
                TodoLogs = Enumerable.Empty<TodoLogDto>()
            };

            var result = controller.Put(1, todo);

            Assert.NotNull(result);
            Assert.False(result.Success);

        }

        [Fact]
        public void DeleteWithSuccess()
        {
            var todoAppService = InstanceOf<ITodoAppService>();
            todoAppService.Delete(Arg.Is(1)).Returns(true);
          
            var result = controller.Delete(1);

            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        [Fact]
        public void DeleteWithoutSuccess()
        {
            var todoAppService = InstanceOf<ITodoAppService>();
            todoAppService.Delete(Arg.Is(1)).Returns(false);

            var result = controller.Delete(1);

            Assert.NotNull(result);
            Assert.False(result.Success);

        }

        [Fact]
        public void DeleteWithoutSuccessByException()
        {
            var todoAppService = InstanceOf<ITodoAppService>();
            todoAppService.Delete(Arg.Is(1)).Returns((callInfo) =>
            {
                throw new Exception();
            });

            var result = controller.Delete(1);

            Assert.NotNull(result);
            Assert.False(result.Success);

        }

        [Fact]
        public void ShouldFailBecauseTextIsEmpty()
        {
            var todoAppService = InstanceOf<ITodoAppService>();

            var todo = new TodoDto
            {
                Id = 1,
                IsCompleted = false,
                Text = null,
                TodoLogs = Enumerable.Empty<TodoLogDto>()
            };

            todoAppService.Create(Arg.Any<TodoDto>()).Returns(todo);

            var result = controller.Post(todo);

            Assert.NotNull(result);
            Assert.False(result.Success);
        }
    }
}
