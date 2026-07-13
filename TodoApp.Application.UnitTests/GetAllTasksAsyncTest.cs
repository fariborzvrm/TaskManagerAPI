using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using TaskManager.Domain.Models;
using TodoApp.Application.UnitTests;

namespace TaskManager.Application.UnitTests
{
    public class GetAllTasksAsyncTest : TaskServiceTests
    {

        [Fact]
        public async Task GetAllTasksAsync_WhenTasksExist_ReturnsListOfTaskDtos()
        {
            var tasks = new List<TaskItem>
            {
                new TaskItem { Id = 1, Title = "Task 1", IsCompleted = true },
                new TaskItem { Id = 2, Title = "Task 2", IsCompleted = false }
            };

            _repoMock.Setup(r => r.GetAllTasksAsync())
                .ReturnsAsync(tasks);

            var result = await _service.GetAllTasksAsync();

            Assert.NotNull(result);

            Assert.Equal(2, result.Count);

            Assert.Collection(result,
                item =>
                {
                    Assert.Equal(1, item.Id);
                    Assert.Equal("Task 1", item.Title);
                    Assert.True(item.IsCompleted);
                },
                item =>
                {
                    Assert.Equal(2, item.Id);
                    Assert.Equal("Task 2", item.Title);
                    Assert.False(item.IsCompleted);
                });
        }

        [Fact]

        public async Task GetAllTasksAsync_WhenNoTasksExist_ReturnsEmptyList()
        {
            var tasks = new List<TaskItem>();

            _repoMock.Setup(r => r.GetAllTasksAsync())
                .ReturnsAsync(tasks);

            var result = await _service.GetAllTasksAsync();

            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
