using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MyToDo.Api.Service;
using RestSharp;
using Newtonsoft.Json;
using MyToDo.Shared.Context;

namespace MyToDo.Service
{
    public class HttpRestClient
    {
        private readonly string apiUrl;
        private readonly RestClient restClient;

        public HttpRestClient(string apiUrl)
        {
            this.apiUrl = apiUrl;
            restClient = new RestClient();
        }

        public async Task<ApiResponse> ExecuteAsync(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseRequest.Method);
            //var request = new RestRequest(apiUrl=baseRequest.Route,baseRequest.Method);新版本写法
            request.AddHeader("Content-Type", baseRequest.ContentType);

            if (baseRequest.Parameter != null)
            {
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);
            }
            restClient.BaseUrl = new Uri(apiUrl + baseRequest.Route);
            var response = await restClient.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<ApiResponse>(response.Content);
        }
        public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);

            if (baseRequest.Parameter != null)
            {
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);
            }
            restClient.BaseUrl = new Uri(apiUrl + baseRequest.Route);
            var response = await restClient.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
        }
    }
}
