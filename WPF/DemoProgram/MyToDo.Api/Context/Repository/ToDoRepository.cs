using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Context.Repository
{
    //public interface IToDoRepository
    //{
    //   Task<bool>  Add(ToDo toDo);
    //   Task<bool> Update(ToDo toDo);
    //   Task<bool> Delete(int id);

    //}
    public class ToDoRepository : Repository<ToDo>,IRepository<ToDo>
    {
        //private MyToDoContext context /*= new MyToDoContext()*/;
        public ToDoRepository(MyToDoContext dbContext ):base(dbContext)
        {
            //this.context = context;
        }
        //public async Task<bool> Add(ToDo toDo)
        //{
        //var reslut=  await  context.ToDo.AddAsync(toDo);
        // return await context.SaveChangesAsync()>0;
        //}

        //public Task<bool> Delete(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> Update(ToDo toDo)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
