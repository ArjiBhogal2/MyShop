using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {

        public IRepository<Product> context;
        public IRepository<ProductCategory> ProductCategories;

        public HomeController(IRepository<Product> ProductContext, IRepository<ProductCategory> ProductCategoryContext)
        {
            context = ProductContext;
            ProductCategories = ProductCategoryContext;
        }

        public ActionResult Details(string iD)
        {
            var product = context.Find(iD);
            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }

        }

        public ActionResult Index(string category=null)
        {

            List<Product> products;
            List<ProductCategory> productcategories = ProductCategories.Collection().ToList();

            if(category==null)
            {
                products = context.Collection().ToList();
            }
            else
            {

                products = context.Collection().Where(p=> p.Category == category).ToList();
            }

            ProductListViewModel productlistviewmodel = new ProductListViewModel();

            productlistviewmodel.ProductCategories = productcategories;
            productlistviewmodel.Products = products;

            
            return View(productlistviewmodel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}