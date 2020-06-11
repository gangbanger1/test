using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestProject.Controllers
{
    abstract public class BaseController<T> : Controller
    {
        protected T Service;
        public BaseController(T service)
        {
            Service = service;
        }
    }
}
