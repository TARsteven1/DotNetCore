using Microsoft.AspNetCore.Mvc;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using MyToDo.Api.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Controllers
{/// <summary>
/// 待办事项控制器
/// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ToDoController : ControllerBase        
    {
        private readonly IToDoService toDoService;
        public ToDoController(IToDoService toDoService)
        {
            this.toDoService = toDoService;
        }

        [HttpGet]
        public async Task<ApiResponse> Get(int id) => await toDoService.GetSingleAsync(id);
        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] ToDoParameters query) => await toDoService.GetAllAsync(query);
        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] ToDoDto model ) => await toDoService.AddAsync(model);
        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] ToDoDto model) => await toDoService.UpdateAsync(model);
        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await toDoService.DeteleAsync(id);
        [HttpGet]
        public async Task<ApiResponse> Summary() => await toDoService.Summary();

    }
}
