using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile() {

            CreateMap<TaskItem, TaskResponseDto>();
            

        }

    }
}
