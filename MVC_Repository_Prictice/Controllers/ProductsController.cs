using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using MVC_Repository_Prictice.Models;
using MVC_Repository_Prictice.Service;
using MVC_Repository_Prictice.Service.Interface;

namespace MVC_Repository_Prictice.Web.Controllers
{
    public class ProductsController : Controller
    {
        private IProductService _productService;

        private ICategoryService _categoryService;

        public IEnumerable<Categories> Categories
        {
            get
            {
                return _categoryService.GetAll();
            }
        }

        public ProductsController(IProductService productService,ICategoryService categoryService)
        {
            this._productService = productService;
            this._categoryService = categoryService;
        }

        //public ActionResult Index()
        //{
        //    var products = productRepository.GetAll()
        //        .OrderByDescending(x => x.ProductID)
        //        .ToList();
        //    return View(products);
        //}
        public ActionResult Index(string category = "all")
        {
            int categoryID = 1;
            ViewBag.CategorySelectList = int.TryParse(category, out categoryID)
                ? this.CategorySelectList(categoryID.ToString())
                : this.CategorySelectList("all");
            var result = category.Equals("all", StringComparison.OrdinalIgnoreCase)
                ? _productService.GetAll()
                : _productService.GetByCategory(categoryID);
            var products = result.OrderByDescending(x => x.ProductID).ToList();
            ViewBag.Category = category;
            return View(products);
        }

        [HttpPost]
        public ActionResult ProductsOfCategory(string category)
        {
            return RedirectToAction("Index", new { category = category });
        }

        /// <summary>
        /// CategorySelectList
        /// </summary>
        /// <param name="selectedValue">The selected value.</param>
        /// <returns></returns>
        public List<SelectListItem> CategorySelectList(string selectedValue = "all")
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem()
            {
                Text = "All Category",
                Value = "all",
                Selected = selectedValue.Equals("all", StringComparison.OrdinalIgnoreCase)
            });
            var categories = _categoryService.GetAll().OrderBy(x => x.CategoryID);
            foreach (var c in categories)
            {
                items.Add(new SelectListItem()
                {
                    Text = c.CategoryName,
                    Value = c.CategoryID.ToString(),
                    Selected = selectedValue.Equals(c.CategoryID.ToString())
                });
            }
            return items;
        }

        //=========================================================================================
        public ActionResult Details(int? id, string category)
        {
            if (!id.HasValue) return RedirectToAction("index");
            Products product = _productService.GetById(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category = string.IsNullOrWhiteSpace(category) ? "all" : category;
            return View(product);
        }

        //=========================================================================================
        public ActionResult Create(string category)
        {
            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName");
            ViewBag.Category = string.IsNullOrWhiteSpace(category) ? "all" : category;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Products products, string category)
        {
            if (ModelState.IsValid)
            {
                this._productService.Create(products);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName", products.CategoryID);
            return View(products);
        }

        //=========================================================================================
        public ActionResult Edit(int? id, string category)
        {
            if (!id.HasValue) return RedirectToAction("index");
            Products product = this._productService.GetById(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.Category = string.IsNullOrWhiteSpace(category) ? "all" : category;
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Products products, string category)
        {
            if (ModelState.IsValid)
            {
                this._productService.Update(products);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName", products.CategoryID);
            return View(products);
        }

        //=========================================================================================
        public ActionResult Delete(int? id, string category)
        {
            if (!id.HasValue) return RedirectToAction("index");
            Products product = this._productService.GetById(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category = string.IsNullOrWhiteSpace(category) ? "all" : category;
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, string category)
        {
            this._productService.Delete(id);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _productService.Dispose();
        //        _categoryService.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}