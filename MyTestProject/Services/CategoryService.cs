using Microsoft.AspNetCore.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models;

namespace MyTestProject.Services
{
    public class CategoryService : BaseService
    {
        public CategoryService(MyDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CategoryModel>> Get()
        {
            return await Context.Categories.ToListAsync();
        }
        
        public async Task<IList<CategoryModel>> GetAll()
        {
            return await Context.Categories.Include(c => c.Color).Include(c => c.Size).ToListAsync();
        }

        public async Task<CategoryModel> GetDetail(int? id)
        {
            return await Context.Categories
                .Include(c => c.Color)
                .Include(c => c.Size)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
        }

        public async Task Add(CategoryModel model)
        {
            Context.Add(model);
            await Context.SaveChangesAsync();
        }

        public async Task Update(CategoryModel model)
        {
            Context.Update(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            var model = Context.Categories.FindAsync(id);
            Context.Remove(model);
            await Context.SaveChangesAsync();
        }

        public bool IsExists(int id)
        {
            return Context.Categories.Any(e => e.CategoryId == id); 
        }
    }
}
