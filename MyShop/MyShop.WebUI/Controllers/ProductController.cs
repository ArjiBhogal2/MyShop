using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModel;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductController : Controller
    {

        public GenericRepository<Product> context;
        public GenericRepository<ProductCategory> ProductCategorycontext; 

        public ProductController ()
        {
            context = new GenericRepository<Product>();
            ProductCategorycontext = new GenericRepository<ProductCategory>();
        }

        // GET: Product
        public ActionResult Index()
        {

            List<Product> products = context.Collection().ToList();

             return View(products);
        }

        public ActionResult Create()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Product = new Product();
            productViewModel.ProductCategories = ProductCategorycontext.Collection();

            return View(productViewModel);

        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
         
            if (!ModelState.IsValid )
            {
                return View(product);

            }
            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }

              
        }

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductViewModel productViewModel = new ProductViewModel();
                productViewModel.Product = product;
                productViewModel.ProductCategories = ProductCategorycontext.Collection();
                
                return View(productViewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product ProductToEdit = context.Find(Id);
            if (ProductToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {

                if (!ModelState.IsValid) {              
                    return View(product);
                }
                else
                {     

                    ProductToEdit.Name = product.Name;
                    ProductToEdit.Description = product.Description;
                    ProductToEdit.Category = product.Category;
                    ProductToEdit.Price = product.Price;
                    ProductToEdit.image = product.image;

                    context.Commit();

                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Delete(string Id)
        {
            Product ProductToDelete = context.Find(Id);
            if (ProductToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ProductToDelete);

            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product ProductToDelete = context.Find(Id);
            if (ProductToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {

                context.Delete(ProductToDelete);
                context.Commit();
                return RedirectToAction("Index");
            }

        }

    }
}