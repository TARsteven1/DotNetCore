using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Context;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using MyToDo.Api.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Controllers
{/// <summary>
/// 账户操作控制器
/// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase      
    {
        private readonly ILoginService Service;
        public LoginController(ILoginService Service)
        {
            this.Service = Service;
        }

        [HttpPost]
        public async Task<ApiResponse> Login( [FromBody] UserDto user ) => await Service.LoginAsync(user.Account, user.PassWord);
        [HttpPost]
        public async Task<ApiResponse> Register([FromBody] UserDto model ) => await Service.Register(model);

    }
}
