using MyShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Core.Contracts
{
    public interface IBasketService

    {
        void AddtoBasket(HttpContextBase httpContext, string productId);
        void RemoveFromBasket(HttpContextBase httpContext, string productId);

        List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext);

         BasketItemSummaryViewModel GetBasketSummary(HttpContextBase httpContext);


    }
}
