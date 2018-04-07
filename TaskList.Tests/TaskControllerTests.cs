using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TaskList.API.Controllers;
using TaskList.API.Database;
using TaskList.API.Models;
using Xunit;

namespace TaskList.Tests
{
    public class TaskControllerTests
    {
        private class Arragement
        {
            public TaskItemAPI Task { get; }

            public TasksController SUT { get; } 

            public Guid Id { get; }

            public Arragement(ITaskDbContext<TaskItem> context, TaskItemAPI task, Guid id)
            {
                Id = id;
                Task = task;
                SUT = new TasksController(context);
            }
        }

        private class ArrangementBuilder
        {
            private TaskItemAPI task;
            private Guid id = Guid.Empty;
            private ITaskDbContext<TaskItem> context = new TaskDbContextStub();

            public ArrangementBuilder WithTaskItem()
            {
                task = new TaskItemAPI()
                {
                    Description = "This is a task for the list",
                    Done = false
                };

                return this;
            }

            public ArrangementBuilder OnRecord()
            {
                var task = new TaskItem()
                {
                    Id = id = Guid.NewGuid(),
                    Description = "This is a task for the list",
                    Done = false
                };

                context.Add(task);
                return this;
            }

            public Arragement Build()
            {
                return new Arragement(context, task, id);
            }
        }

        [Fact]
        public void Ctor_WithNullTaskDbContext_ShouldThrowException()
        {
            // Act 
            var error = Record.Exception(() => new TasksController(null));

            // Assert

            error.Should().BeOfType<ArgumentNullException>();
            error.Message.Should().Be("Value cannot be null.\r\nParameter name: context");
        }

        [Fact]
        public async Task Add_WithValidModel_ShouldReturnGuid()
        {
            // Act 
            var arrangement = new ArrangementBuilder()
                .WithTaskItem()
                .Build();

            // Arrange
            var result = await arrangement.SUT.Post(arrangement.Task);


            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var resultObject = result as OkObjectResult;
            resultObject.Value.Should().BeOfType<Guid>();
        }

        [Fact]
        public async Task Put_WithNoRecord_ShouldReturnNotFound()
        {
            // Act 
            var arrangement = new ArrangementBuilder()
                .WithTaskItem()
                .Build();

            // Arrange
            var result = await arrangement.SUT.Put(arrangement.Id ,arrangement.Task);


            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
