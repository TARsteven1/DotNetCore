using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using MyToDo.Shared.Context;
using System.Threading.Tasks;

namespace MyToDo.Api.Service
{/// <summary>
/// 备忘录的实现
/// </summary>
    public class MemoService : IMemoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MemoService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> AddAsync(MemoDto model)
        {
            try
            {
                var dbMemo = mapper.Map<Memo>(model);

                await unitOfWork.GetRepository<Memo>().InsertAsync(dbMemo);
                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, dbMemo);
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
                var repository = unitOfWork.GetRepository<Memo>();
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
                var repository = unitOfWork.GetRepository<Memo>();
                //实现分页查询
                var todos = await repository.GetPagedListAsync(predicate:
                x=>    string.IsNullOrWhiteSpace(query.Search)?true:x.Title.Equals(query.Search),
                pageIndex:query.PageIndex,pageSize:query.PageSize
                ,orderBy:source=> source.OrderByDescending(t=>t.CreateTime)/*根据时间排序*/);
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

        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            try
            {
                var repository = unitOfWork.GetRepository<Memo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));

                return new ApiResponse(true, todo);

            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(MemoDto model)
        {
            try
            {
                var dbToDo = mapper.Map<Memo>(model);
                var repository = unitOfWork.GetRepository<Memo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(dbToDo.Id));
                todo.Title = dbToDo.Title;
                todo.Content = dbToDo.Content;
                todo.UpdateTime = DateTime.Now;
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
