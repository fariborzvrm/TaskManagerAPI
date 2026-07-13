using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs;
using TaskManager.Application.Exceptions;
using TaskManager.Domain.Models;
using TodoApp.Application.UnitTests;

namespace TaskManager.Application.UnitTests
{
    public class UpdateTaskAsyncTest : TaskServiceTests
    {
        [Fact]

        public async Task UpdateTaskAsync_WhenTaskDoesNotExist_ThrowsNotFoundException()
        {
            int taskId = 1;            
            var dto = new UpdateTaskDto {Title = "Existing Task", IsCompleted = false };

            _repoMock
                .Setup(r => r.GetByIdAsync(taskId))
                .ReturnsAsync((TaskItem?)null);

            await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateTaskAsync(taskId,dto));

            _uowMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]

        public async Task UpdateTaskAsync_WhenTaskExistsWithAnotherId_ThrowsDuplicateTaskTitleException()
        {
            int taskId = 1;
            var taskDto = new UpdateTaskDto { Title = "Updated Task", IsCompleted = false };
            _repoMock
                .Setup(r => r.ExistsByTitleExceptIdAsync(taskDto.Title, taskId))
                .ReturnsAsync(true);
            
            await Assert.ThrowsAsync<DuplicateTaskTitleException>(() => _service.UpdateTaskAsync(taskId, taskDto));

            _uowMock.Verify(u => u.SaveChangesAsync(), Times.Never);


        }

        [Fact]

        public async Task UpdateTaskAsync_WhenTitleIsUnique_UpdatesTaskSuccessfully()
        {
            int taskId = 1;
            var task = new TaskItem { Id = taskId, Title = "Old", IsCompleted = true };
            var taskDto = new UpdateTaskDto { Title = "Updated Task", IsCompleted = false };

            _repoMock.Setup(r => r.GetByIdAsync(taskId))
                     .ReturnsAsync(task);

            _repoMock
                .Setup(r => r.ExistsByTitleExceptIdAsync(taskDto.Title, taskId))
                .ReturnsAsync(false);

           

            var result = await _service.UpdateTaskAsync(taskId, taskDto);

            Assert.Equal("Updated Task", task.Title);
            Assert.True(task.IsCompleted);

            _repoMock.Verify(r => r.UpdateTaskAsync(taskId, It.IsAny<TaskItem>()), Times.Once);
            _uowMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }


    }
}
