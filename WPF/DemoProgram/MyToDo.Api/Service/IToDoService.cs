using MyToDo.Api.Context;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Service
{
    public interface IToDoService :IBaseService<ToDoDto>
    {
        Task<ApiResponse> GetAllAsync(ToDoParameters query);
        Task<ApiResponse> Summary();
    }
}
