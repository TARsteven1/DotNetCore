using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Context
{
    public class MyToDoContext:DbContext
    {
        public DbSet<ToDo> ToDo { set; get; }
        public DbSet<Memo> Memo { set; get; }
        public DbSet<User> User { set; get; }
        public MyToDoContext(DbContextOptions<MyToDoContext> options):base(options)
        {

        }
    }
}
