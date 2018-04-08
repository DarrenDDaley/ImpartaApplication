﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void TaskItemAPIModel_WithInvaildDescription_ShouldReturnValidationError(string input)
        {
            // Arrange
            var arrangement = new TaskItemAPI() { Description = input, Done = true };

            // Act 
            var validationContext = new ValidationContext(arrangement);

            var result = arrangement.Validate(validationContext);

            // Assert
            result.Count().Should().Be(1);
            result.First().ErrorMessage.Should().Be("Description can't be empty or null.");
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
        public async Task Edit_WithNoRecord_ShouldReturnNotFound()
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

        [Fact]
        public async Task Edit_WithRecord_ShouldReturnOk()
        {
            // Act 
            var arrangement = new ArrangementBuilder()
                .WithTaskItem()
                .OnRecord()
                .Build();

            // Arrange
            var result = await arrangement.SUT.Put(arrangement.Id, arrangement.Task);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task Done_WithNoRecord_ShouldReturnNotFound()
        {
            // Act 
            var arrangement = new ArrangementBuilder()
                .WithTaskItem()
                .Build();

            // Arrange
            var result = await arrangement.SUT.Put(arrangement.Id, true);


            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Done_WithRecord_ShouldReturnOk()
        {
            // Act 
            var arrangement = new ArrangementBuilder()
                .WithTaskItem()
                .OnRecord()
                .Build();

            // Arrange
            var result = await arrangement.SUT.Put(arrangement.Id, true);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task Delete_WithNoRecord_ShouldReturnNotFound()
        {
            // Act 
            var arrangement = new ArrangementBuilder()
                .WithTaskItem()
                .Build();

            // Arrange
            var result = await arrangement.SUT.Delete(arrangement.Id);


            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Delete_WithRecord_ShouldReturnOk()
        {
            // Act 
            var arrangement = new ArrangementBuilder()
                .WithTaskItem()
                .OnRecord()
                .Build();

            // Arrange
            var result = await arrangement.SUT.Delete(arrangement.Id);

            // Assert
            result.Should().BeOfType<OkResult>();
        }
    }
}
