using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class GenericRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items; 
        string className;


       public GenericRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;
            if (items == null)
            {
                items = new List<T>();

            }
        }

        public void Commit()
        {
            cache[className] = items;

        }

        public void Insert(T item)
        {
            items.Add(item);
        }

        public void Update(T item)
        {
            T itemToUpdate = items.Find(i => i.ID == item.ID);

            if (item != null)
            {
                itemToUpdate = item;
            }
            else
            {
                throw new Exception(this.className + " not found.");
            }
        }

        public T Find(String Id)
        {
            T itemToFind = items.Find(i => i.ID == Id);

            if(itemToFind != null)
            {
                return itemToFind;
            }
            else
            {
                throw new Exception(this.className + " not found.");
            }
        }

        public void Delete(T item)
        {
            T productToDelete = items.Find(i => i.ID == item.ID);

            if(productToDelete !=null)
            {
                items.Remove(productToDelete);
            }
            else
            {
                throw new Exception(this.className + " not found.");
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

    }






    //    public IQueryable<ProductCategory> Collection()
    //    {
    //        return productCategories.AsQueryable();
    //    }


    //}
}
