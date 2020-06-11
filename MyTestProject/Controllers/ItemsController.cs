using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyTestProject.Services;
using TestProject.Models;

namespace MyTestProject.Controllers
{
    public class ItemsController : BaseController<ItemService>
    {
        private readonly CategoryService categoryService;
        private readonly ParameterService parameterService;
        public ItemsController(ItemService service, ParameterService _parameterService, CategoryService _categoryService) : base(service)
        {
            parameterService = _parameterService;
            categoryService = _categoryService;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            return View(await Service.GetItemsList());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemsModel = await Service.Get(id);
            if (itemsModel == null)
            {
                return NotFound();
            }

            return View(itemsModel);
        }

        // GET: Items/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await categoryService.Get(), "CategoryId", "Name");
            ViewBag.SizeId = new SelectList(await parameterService.GetSizes(), "SizeId", "Size");
            ViewBag.ColorId = new SelectList(await parameterService.GetColors(), "ColorId", "Color");
            ViewBag.SizeIdCopy = ViewBag.SizeId;
            ViewBag.ColorIdCopy = ViewBag.ColorId;
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,CategoryId")] ItemsModel itemsModel)
        {
            if (ModelState.IsValid)
            {
                await Service.Add(itemsModel);
                return RedirectToAction(nameof(Index));
            }
            return View(itemsModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemsModel = await Service.Get(id);
            if (itemsModel == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await categoryService.Get(), "CategoryId", "CategoryId", itemsModel.CategoryId);
            
            return View(itemsModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,CategoryId")] ItemsModel itemsModel)
        {
            if (id != itemsModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var success = await Service.Edit(itemsModel);
                if (!ItemsModelExists(itemsModel.Id))
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await categoryService.Get(), "CategoryId", "CategoryId", itemsModel.CategoryId) ;
            return View(itemsModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemsModel = await Service.Get(id);
            if (itemsModel == null)
            {
                return NotFound();
            }

            return View(itemsModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await Service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ItemsModelExists(int id)
        {
            return Service.IsExist(id);
        }
    }
}
