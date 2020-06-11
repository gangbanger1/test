using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models;

namespace MyTestProject.Services
{
    public class ParameterService : BaseService
    {
        public ParameterService(MyDbContext context) : base(context)
        {
        }
     
        public async Task<IEnumerable<SizeModel>> GetSizes()
        {
            return await Context.Size.ToListAsync();
        }

        public async Task<IEnumerable<ColorModel>> GetColors()
        {
            return await Context.Color.ToListAsync();
        }
    }
}
