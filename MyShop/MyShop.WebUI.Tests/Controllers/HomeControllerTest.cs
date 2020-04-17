using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop.WebUI;
using MyShop.WebUI.Controllers;
using MyShop.WebUI.Tests.Mocks;
using MyShop.Core.Models;
using MyShop.Core.Contracts;
using MyShop.Core.ViewModel;

namespace MyShop.WebUI.Tests.Controllers
{
	[TestClass]
	public class HomeControllerTest
	{
		[TestMethod]
		public void IndexPageDoesReturnProduct()


		{

			IRepository<Product> ProductMockContext = new mockContext<Product>();
			IRepository<ProductCategory> ProductCategoryMockContext = new mockContext<ProductCategory>();


		  HomeController homeController = new HomeController(ProductMockContext, ProductCategoryMockContext );

			ProductMockContext.Insert(new Product());

			var result = homeController.Index() as ViewResult;
			var prodListResult = (ProductListViewModel)homeController.ViewData.Model;

			Assert.AreEqual(1, prodListResult.Products.Count());



		}


	}
}
