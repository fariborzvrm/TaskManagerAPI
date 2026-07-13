using AutoMapper;
using Moq;
using TaskManager.Application.DTOs;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Domain.Models;

namespace TodoApp.Application.UnitTests
{
    public class TaskServiceTests
    {
       public  readonly Mock<ITaskRepository> _repoMock;
        public readonly Mock<IUnitOfWork> _uowMock;
        public readonly Mock<IMapper> _mapperMock;
        public readonly TaskService _service;

        public TaskServiceTests()
        {
            _repoMock = new Mock<ITaskRepository>();
            _uowMock = new Mock<IUnitOfWork>();
            _service = new TaskService(_repoMock.Object, _uowMock.Object , _mapperMock.Object);
        }



        


    }
}
