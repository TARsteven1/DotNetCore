using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyToDo.Api.Service
{/// <summary>
/// 待办事项的实现
/// </summary>
    public class ToDoService : IToDoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ToDoService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> AddAsync(ToDoDto model)
        {
            try
            {
                var dbToDo = mapper.Map<ToDo>(model);

                await unitOfWork.GetRepository<ToDo>().InsertAsync(dbToDo);
                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, dbToDo);
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

        public async Task<ApiResponse> GetAllAsync(QueryParameter query)
        {
            try
            {
                var repository = unitOfWork.GetRepository<ToDo>();
                //实现分页查询
                var todos = await repository.GetPagedListAsync(predicate:
                x => string.IsNullOrWhiteSpace(query.Search) ? true : x.Title.Contains(query.Search),
                pageIndex: query.PageIndex, pageSize: query.PageSize
                , orderBy: source => source.OrderByDescending(t => t.CreateTime)/*根据时间排序*/);

                //var todos = await repository.GetAllAsync();
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

        public async Task<ApiResponse> GetAllAsync(ToDoParameters query)
        {
            try
            {
                var repository = unitOfWork.GetRepository<ToDo>();
                //实现分页查询
                var todos = await repository.GetPagedListAsync(predicate:
                x => (string.IsNullOrWhiteSpace(query.Search) ? true : x.Title.Contains(query.Search))
                && (query.Status==null ? true:x.Status.Equals(query.Status)),
                pageIndex: query.PageIndex, pageSize: query.PageSize
                , orderBy: source => source.OrderByDescending(t => t.CreateTime)/*根据时间排序*/);

                return new ApiResponse(true, todos);

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

        public async Task<ApiResponse> Summary()
        {
            try
            {
                //拿到待办事项结果
                var todos = await unitOfWork.GetRepository<ToDo>().GetAllAsync(
                    orderBy :source => source.OrderByDescending(t => t.CreateTime));
                //拿到备忘录结果
                var memos = await unitOfWork.GetRepository<Memo>().GetAllAsync(
                   orderBy: source => source.OrderByDescending(t => t.CreateTime));
               //统计部分
                SummaryDto summary = new SummaryDto();
                summary.Sum = todos.Count();
                summary.FinishedCount = todos.Where(t => t.Status == 1).Count();
                summary.FinishedRatio = (summary.FinishedCount / (double)summary.Sum).ToString("0%");
                summary.MemoCount = memos.Count();
                summary.ToDoList = new ObservableCollection<ToDoDto>(mapper.Map<List<ToDoDto>>(todos.Where(t=>t.Status==0)));
                summary.MemoList = new ObservableCollection<MemoDto>(mapper.Map<List<MemoDto>>(memos));

                return new ApiResponse(true, summary);
            }
            catch (Exception)
            {

                return new ApiResponse(false,"");
            }
        }

        public async Task<ApiResponse> UpdateAsync(ToDoDto model)
        {
            try
            {
                var dbToDo = mapper.Map<ToDo>(model);
                var repository = unitOfWork.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(dbToDo.Id));
                todo.Title = dbToDo.Title;
                todo.Content = dbToDo.Content;
                todo.UpdateTime = DateTime.Now;
                todo.Status = dbToDo.Status;
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
