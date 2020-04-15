using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModel
{
    public class BasketItemSummaryViewModel
    {
        public int BasketCount { get; set; }
        public decimal BasketTotal { get; set; }

        public BasketItemSummaryViewModel()
        {

        }
         public BasketItemSummaryViewModel(int BasketCount, decimal BasketTotal)
        {
            this.BasketCount = BasketCount;
            this.BasketTotal = BasketTotal;
        }

    }
}
