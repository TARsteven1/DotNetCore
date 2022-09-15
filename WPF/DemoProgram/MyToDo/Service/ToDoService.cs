using MyToDo.Shared;
using MyToDo.Shared.Context;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
   public class ToDoService: BaseService<ToDoDto>, IToDoService
    {
        private readonly HttpRestClient client;

        public ToDoService(HttpRestClient client):base(client ,"ToDo")
        {
            this.client = client;
        }

        public async Task<ApiResponse<PagedList<ToDoDto>>> GetAllFilterAsync(ToDoParameters parameters)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.GET;
            //request.Route = $"api/{serviceName}/GetAll";
            request.Route = $"api/ToDo/GetAll?pageIndex={parameters.PageIndex}" + $"&pageSize={parameters.PageSize}" + $"&search={parameters.Search}" + $"&status={parameters.Status}";
            //return await client.ExecuteAsync<ApiResponse<PagedList<TEntity>>>(request);
            return await client.ExecuteAsync<PagedList<ToDoDto>>(request);
        }

        public async Task<ApiResponse<SummaryDto>> SummaryAsync()
        {
            BaseRequest request = new BaseRequest();
            request.Route = "api/ToDo/Summary";
            return await client.ExecuteAsync<SummaryDto>(request);
        }
    }
}
