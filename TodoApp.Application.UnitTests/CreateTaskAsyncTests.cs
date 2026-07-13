using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.Application.UnitTests;
using Moq;
using TaskManager.Application.DTOs;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Domain.Models;

namespace TaskManager.Application.UnitTests
{
    public class CreateTaskAsyncTests : TaskServiceTests
    {
        [Fact]
        public async Task CreateTaskAsync_WhenTitleExists_ThrowsDuplicateTaskTitleExceptions()
        {
            var dto = new CreateTaskDto { Title = "Existing Task" };

            _repoMock
                .Setup(r => r.TaskExistsByTitleAsync(dto.Title))
                .ReturnsAsync(true);

            await Assert.ThrowsAsync<DuplicateTaskTitleException>(()
                => _service.CreateTaskAsync(dto));

        }

        [Fact]

        public async Task CreateAsync_WhenTitleIsUnique_CreatesTask()
        {
            var dto = new CreateTaskDto { Title = "Existing Task" };

            _repoMock
                .Setup(r => r.TaskExistsByTitleAsync(dto.Title))
                .ReturnsAsync(false);

            var result = await _service.CreateTaskAsync(dto);

            _repoMock.Verify(r => r.CreateTaskAsync(It.IsAny<TaskItem>()), Times.Once);
            _uowMock.Verify(u => u.SaveChangesAsync(), Times.Once);

        }
    }
}
