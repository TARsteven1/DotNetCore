using System;
using AutoMapper.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Api.Dtos;

namespace MyToDo.Api.Extensions
{/// <summary>
/// 配置数据传输层实体类映射
/// </summary>
    public class AutoMapperProFile : MapperConfigurationExpression
    {
        public AutoMapperProFile()
        {
            CreateMap<ToDo, ToDoDto>().ReverseMap();
        }
    }
}
