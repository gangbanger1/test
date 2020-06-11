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
    public class CategoryController : BaseController<CategoryService>
    {
        private readonly ParameterService parameterService;
        public CategoryController(CategoryService service,ParameterService _parameterService) : base(service)
        {
            parameterService = _parameterService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await Service.GetAll());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categoryModel = await Service.GetDetail(id);
            if (categoryModel == null)
            {
                return NotFound();
            }
            return View(categoryModel);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["ColorId"] = new SelectList(await parameterService.GetColors(), "ColorId", "Color");
            ViewData["SizeId"] = new SelectList(await parameterService.GetSizes(), "SizeId", "Size");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Name,SizeId,ColorId")] CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                await Service.Add(categoryModel);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColorId"] = new SelectList(await parameterService.GetColors(), "ColorId", "Color", categoryModel.Color.ColorId);
            ViewData["SizeId"] = new SelectList(await parameterService.GetSizes(), "SizeId", "Size", categoryModel.Size.SizeId);
            return View(categoryModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryModel = await Service.GetDetail(id);
            if (categoryModel == null)
            {
                return NotFound();
            }
            ViewData["ColorId"] = new SelectList(await parameterService.GetColors(), "ColorId", "ColorId", categoryModel.ColorId);
            ViewData["SizeId"] = new SelectList(await parameterService.GetSizes(), "SizeId", "SizeId", categoryModel.SizeId);
            return View(categoryModel);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name,SizeId,ColorId")] CategoryModel categoryModel)
        {
            if (id != categoryModel.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Service.Update(categoryModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryModelExists(categoryModel.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColorId"] = new SelectList(await parameterService.GetColors(), "ColorId", "ColorId", categoryModel.ColorId);
            ViewData["SizeId"] = new SelectList(await parameterService.GetSizes(), "SizeId", "SizeId", categoryModel.SizeId);
            return View(categoryModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categoryModel = await Service.GetDetail(id);
            if (categoryModel == null)
            {
                return NotFound();
            }
            return View(categoryModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await Service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryModelExists(int id)
        {
            return Service.IsExists(id);
        }
    }
}
