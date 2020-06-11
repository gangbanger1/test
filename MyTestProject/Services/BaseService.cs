using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models;

namespace MyTestProject.Services
{
    public abstract class BaseService
    {
        public BaseService(MyDbContext context)
        {
            Context = context;
        }

        protected MyDbContext Context;
    }
}
