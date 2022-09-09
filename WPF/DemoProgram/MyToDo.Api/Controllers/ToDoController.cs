using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Context;
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
        public async Task<ApiResponse> GetAll(int id) => await toDoService.GetAllAsync();
        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] ToDo model ) => await toDoService.AddAsync(model);
        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] ToDo model) => await toDoService.UpdateAsync(model);
        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await toDoService.DeteleAsync(id);

    }
}
