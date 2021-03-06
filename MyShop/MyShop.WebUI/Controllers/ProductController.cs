﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModel;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Contracts;
using System.IO;

namespace MyShop.WebUI.Controllers
{
    public class ProductController : Controller
    {

        public IRepository<Product> context;
        public IRepository<ProductCategory> ProductCategories; 

        public ProductController (IRepository<Product> ProductContext, IRepository<ProductCategory> ProductCategoryContext )
        {
            context = ProductContext;
            ProductCategories = ProductCategoryContext;
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
            productViewModel.ProductCategories = ProductCategories.Collection();

            return View(productViewModel);

        }
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
         
            if (!ModelState.IsValid )
            {
                return View(product);

            }
            else
            {
                if(file != null)
                {
                    product.image = product.ID + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImage//") + product.image);
                }

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
                productViewModel.ProductCategories = ProductCategories.Collection();
                
                return View(productViewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file)
        {
            Product ProductToEdit = context.Find(Id);
            if (ProductToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {

                if (!ModelState.IsValid) {
                    ProductViewModel productViewModel = new ProductViewModel();
                    productViewModel.Product = product;
                    productViewModel.ProductCategories = ProductCategories.Collection();

                    return View(productViewModel);
                }
                else
                {
                    if (file != null)
                    {
                        ProductToEdit.image = product.ID + Path.GetExtension(file.FileName);
                        file.SaveAs(Server.MapPath("//Content//ProductImage//") + ProductToEdit.image);
                    }
                    ProductToEdit.Name = product.Name;
                    ProductToEdit.Description = product.Description;
                    ProductToEdit.Category = product.Category;
                    ProductToEdit.Price = product.Price;


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

                context.Delete(ProductToDelete.ID);
                context.Commit();
                return RedirectToAction("Index");
            }

        }

    }
}