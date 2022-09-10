using MyToDo.Api.Context;
using MyToDo.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Service
{
    public interface IMemoService : IBaseService<MemoDto>
    {
    }
}
