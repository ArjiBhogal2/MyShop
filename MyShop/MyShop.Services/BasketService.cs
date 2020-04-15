using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;
using MyShop.Core.Contracts;
using System.Web;

namespace MyShop.Services
{
    public class BasketService
    {
        IRepository<Product> productContext;
        IRepository<Basket> basketContext;

        public const string BasketSessionName = "eCommerceBasket";

        public BasketService(IRepository<Product> ProductContext, IRepository<Basket> BasketContext)
        {
            this.productContext = ProductContext;
            this.basketContext = BasketContext;

        }

        private Basket GetBasket(HttpContextBase httpContext, bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);

            Basket basket = new Basket();

            if (cookie != null)
            {
                string basketId = cookie.Value;
                if(!string.IsNullOrEmpty(basketId))
                {
                    basket = basketContext.Find(basketId);
                }
                else
                {
                    if(createIfNull)
                    {
                        basket = CreateNewBasket(httpContext);
                    }

                }

            }
            else
            if(createIfNull)
            {
                basket = CreateNewBasket(httpContext);
            }
            return basket;
        }

        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            basketContext.Insert(basket);
            basketContext.Commit();

            HttpCookie cookie = new HttpCookie(BasketSessionName);
            cookie.Value = basket.ID;
            cookie.Expires = DateTime.Now.AddDays(7);
            httpContext.Response.Cookies.Add(cookie);

            return basket;

        }

        public void AddtoBasket(HttpContextBase httpContext, string productId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductID == productId);
            
            if(item == null)
            {
                item = new BasketItem()
                {
                    BasketID = basket.ID,
                    ProductID = productId,
                    Quantity = 1
                };

                basket.BasketItems.Add(item);

            }
            else
            {
                item.Quantity = item.Quantity + 1;
            }

            basketContext.Commit();
        }
        public void RemoveFromBasket(HttpContextBase httpContext, string productId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductID == productId);

            if (item != null)
            {
                basket.BasketItems.Remove(item);
                basketContext.Commit();
            }
        }


    }
}
