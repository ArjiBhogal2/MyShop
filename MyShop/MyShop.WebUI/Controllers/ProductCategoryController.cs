using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Contracts;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryController : Controller
    {

        IRepository<ProductCategory> context;

        public ProductCategoryController(IRepository<ProductCategory> ProductCategoryContext)
        {
            context = ProductCategoryContext;
        }

        // GET: Product
        public ActionResult Index()
        {

            List<ProductCategory> productCategories = context.Collection().ToList();

            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();

            return View(productCategory);

        }
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {

            if (!ModelState.IsValid)
            {
                return View(productCategory);

            }
            else
            {
                context.Insert(productCategory);
                context.Commit();

                return RedirectToAction("Index");
            }


        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productCategory = context.Find(Id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {

                return View(productCategory);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string Id)
        {
            ProductCategory ProductCategoryToEdit = context.Find(Id);
            if (ProductCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {

                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }
                else
                {

                    ProductCategoryToEdit.Name = productCategory.Name;
                    ProductCategoryToEdit.Description = productCategory.Description;
                 

                    context.Commit();

                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory ProductCategoryToDelete = context.Find(Id);
            if (ProductCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ProductCategoryToDelete);

            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory ProductCategoryToDelete = context.Find(Id);
            if (ProductCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {

                context.Delete(ProductCategoryToDelete);
                context.Commit();
                return RedirectToAction("Index");
            }

        }

    }
}