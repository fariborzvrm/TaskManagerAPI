using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using TaskManager.Application.Exceptions;
using TaskManager.Domain.Models;
using TodoApp.Application.UnitTests;

namespace TaskManager.Application.UnitTests
{
    public class GetByIdAsyncTest : TaskServiceTests
    {
        [Fact]

        public async Task GetByIdAsync_WhenTaskExists_ReturnsTaskDto()
        {
            int taskId = 1;
            var task = new TaskItem {Id= taskId, Title="Task", IsCompleted=true };

            _repoMock.Setup(r => r.GetByIdAsync(taskId))
                .ReturnsAsync(task);

            var result = _service.GetByIdAsync(taskId);

            Assert.NotNull(result);
            Assert.Equal("Task",task.Title);
        }

        [Fact]
        public async Task GetByIdAsync_WhenTaskDoesNotExist_ThrowsNotFoundException()
        {
            int id = 99;
            
            _repoMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync((TaskItem?)null);

            await Assert.ThrowsAsync<NotFoundException>(() =>
            _service.GetByIdAsync(id));
            
        }
    }
}
