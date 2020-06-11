using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models;

namespace MyTestProject.Services
{
    public class ItemService : BaseService
    {
        public ItemService(MyDbContext context) : base(context)
        {
        }

        public async Task<IList<ItemsModel>> GetItemsList()
        {
            return await Context
                .ItemsModel
                .Include(i => i.Category)
                .ToListAsync();
        }

        public async Task<ItemsModel> Get(int? id)
        {
            return await Context.ItemsModel
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> Add(ItemsModel model)
        {
            Context.Add(model);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(ItemsModel model)
        {
            Context.Update(model);
            await Context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int? id)
        {
            var model = await Context.ItemsModel.FindAsync(id);
            Context.Remove(model);
            await Context.SaveChangesAsync();
            return true;
        }

        public bool IsExist(int? id)
        {
            return Context.ItemsModel.Any(e => e.Id == id);
        }
    }
}
