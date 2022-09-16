using MyToDo.Shared.Context;
using MyToDo.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public interface ILoginService
    {
        Task<ApiResponse> LoginAsync(UserDto user);
        Task<ApiResponse> Register(UserDto user);
    }
}
