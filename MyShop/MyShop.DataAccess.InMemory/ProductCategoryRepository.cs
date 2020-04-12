using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;


namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;

        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["ProductCategories"] as List<ProductCategory>;

            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();

            }
        }

        public void Commit()
        {
            cache["ProductCategories"] = productCategories;
        }

        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);

        }

        public void Update(ProductCategory productCategory)
        {
            ProductCategory productCategoryToUpdate = productCategories.Find(p => p.ID == productCategory.ID);

            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product not found.");
            }


        }

        public ProductCategory Find(string ID)
        {
            ProductCategory productCategoryToFind = productCategories.Find(p => p.ID == ID);

            if (productCategoryToFind != null)
            {
                return productCategoryToFind;
            }
            else
            {
                throw new Exception("Product not found.");
            }

        }

        public void Delete(ProductCategory productCategory)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.ID == productCategory.ID);

            if (productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product not found.");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }


    }

}

