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
    public class MemoController : ControllerBase      
    {
        private readonly IMemoService Service;
        public MemoController(IMemoService toDoService)
        {
            this.Service = toDoService;
        }

        [HttpGet]
        public async Task<ApiResponse> Get(int id) => await Service.GetSingleAsync(id);
        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] QueryParameter query) => await Service.GetAllAsync(query);
        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] MemoDto model ) => await Service.AddAsync(model);
        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] MemoDto model) => await Service.UpdateAsync(model);
        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await Service.DeteleAsync(id);

    }
}
