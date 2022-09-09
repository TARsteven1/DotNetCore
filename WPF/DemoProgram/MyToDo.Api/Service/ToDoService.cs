using MyToDo.Api.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Service
{/// <summary>
/// 待办事项的实现
/// </summary>
    public class ToDoService : IToDoService
    {
        private readonly IUnitOfWork unitOfWork;

        public ToDoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse> AddAsync(ToDo model)
        {
            try
            {
                await unitOfWork.GetRepository<ToDo>().InsertAsync(model);
                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, model);
                }
                return new ApiResponse(false, "添加数据失败!");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }

        }

        public async Task<ApiResponse> DeteleAsync(int id)
        {
            try
            {
                var repository = unitOfWork.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                repository.Delete(todo);

                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, "");
                }
                return new ApiResponse(false, "删除数据失败!");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            try
            {
                var repository = unitOfWork.GetRepository<ToDo>();
                var todos = await repository.GetAllAsync();
                //var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                //repository.Delete(todo);

                //if (await unitOfWork.SaveChangesAsync() > 0)
                //{
                return new ApiResponse(true, todos);
                //}
                //return new ApiResponse(false, "删除数据失败!");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            try
            {
                var repository = unitOfWork.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));

                return new ApiResponse(true, todo);

            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(ToDo model)
        {
            try
            {
                var repository = unitOfWork.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(model.Id));
                todo.Title = model.Title;
                todo.Content = model.Content;
                todo.UpdateTime = DateTime.Now;
                todo.Status = model.Status;
                repository.Update(todo);

                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, todo);
                }
                return new ApiResponse(false, "更新数据异常!");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
    }
}
