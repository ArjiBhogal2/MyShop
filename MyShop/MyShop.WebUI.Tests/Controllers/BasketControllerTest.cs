using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop.WebUI.Controllers;
using MyShop.Services;
using MyShop.Core.Contracts;
using MyShop.WebUI.Tests.Mocks;
using MyShop.Core.Models;
using MyShop.Core.ViewModel;
using System.Web;
using System.Linq;
using System.Web.Mvc;

namespace MyShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTest
    {
        [TestMethod]
        public void CanAddtoBasketService()
        {
            //Arrange
            IRepository<Product> productContext = new mockContext<Product>();
            IRepository<ProductCategory> productCategoryContext = new mockContext<ProductCategory>();
            IRepository<Basket> baskets = new mockContext<Basket>();
            var httpcontext = new MockHttpContext();

            
            IBasketService basketService = new BasketService(productContext, baskets);
            
            

            //Act
            basketService.AddtoBasket(httpcontext, "2");
            Basket basket = baskets.Collection().FirstOrDefault();

            //Assert
            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.BasketItems.Count());
            Assert.AreEqual("2", basket.BasketItems.ToList().FirstOrDefault().ProductID);


        }

        [TestMethod]
        public void CanAddtoBasketController()
        {
            IRepository<Product> productContext = new mockContext<Product>();
            IRepository<ProductCategory> productCategoryContext = new mockContext<ProductCategory>();
            IRepository<Basket> baskets = new mockContext<Basket>();
            var httpcontext = new MockHttpContext();
            
            IBasketService basketService = new BasketService(productContext, baskets);
            var controller = new BasketController(basketService);
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpcontext, new System.Web.Routing.RouteData(), controller);

            //Act
            controller.AddToBasket("2");

            Basket basket = baskets.Collection().FirstOrDefault();

            //Assert
            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.BasketItems.Count());
            Assert.AreEqual("2", basket.BasketItems.ToList().FirstOrDefault().ProductID);
                             }

        [TestMethod]
        public void CanCalculateBasketController()
        {

            //Arrange
            IRepository<Product> productContext = new mockContext<Product>();
            IRepository<ProductCategory> productCategoryContext = new mockContext<ProductCategory>();
            IRepository<Basket> basketsContext = new mockContext<Basket>();
            var httpcontext = new MockHttpContext();

            productContext.Insert(new Product { ID = "1", Price = 10.00m });
            productContext.Insert(new Product { ID = "2", Price = 5.00m });
            Basket basket = new Basket();

            basket.BasketItems.Add(new BasketItem { ID = "1", ProductID="1",  Quantity = 2 });
            basket.BasketItems.Add(new BasketItem { ID = "2", ProductID = "2" ,Quantity = 3 });

            basketsContext.Insert(basket);

            IBasketService basketService = new BasketService(productContext, basketsContext);

            var basketController = new BasketController(basketService);

            httpcontext.Response.Cookies.Add(new HttpCookie("eCommerceBasket") { Value = basket.ID });

            basketController.ControllerContext = new System.Web.Mvc.ControllerContext(httpcontext, new System.Web.Routing.RouteData(), basketController);

            //Act

            var result = basketController.BasketSummary() as PartialViewResult;
            var basketSummary = (BasketItemSummaryViewModel)result.ViewData.Model;


            //
            Assert.AreEqual(5, basketSummary.BasketCount);
            Assert.AreEqual(35.00m, basketSummary.BasketTotal);
            
        }

    }
}
