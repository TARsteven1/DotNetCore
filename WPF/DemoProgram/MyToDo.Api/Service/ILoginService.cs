using MyToDo.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using MyToDo.Shared.Context;
using System.Threading.Tasks;

namespace MyToDo.Api.Service
{
    public interface ILoginService
    {
        Task<ApiResponse> LoginAsync(string Account, string Password);
        Task<ApiResponse> Register(UserDto user);
    }
}
