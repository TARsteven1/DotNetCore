using MyToDo.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using MyToDo.Shared.Context;
using System.Threading.Tasks;

namespace MyToDo.Api.Service
{
    public interface IBaseService<T>
    {
        Task<ApiResponse> GetAllAsync(QueryParameter query);
        Task<ApiResponse> GetSingleAsync(int id);
        Task<ApiResponse> AddAsync(T model);
        Task<ApiResponse> UpdateAsync(T model);
        Task<ApiResponse> DeteleAsync(int id);

    }
}
