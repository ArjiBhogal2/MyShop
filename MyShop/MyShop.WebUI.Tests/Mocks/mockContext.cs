using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.WebUI.Tests.Mocks
{
    public class mockContext<T> : IRepository<T> where T : BaseEntity 
    {

        List<T> items;
        string className;

        public mockContext()
        {

                items = new List<T>();

        }

        public void Commit()
        {
            return;

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

            if (itemToFind != null)
            {
                return itemToFind;
            }
            else
            {
                throw new Exception(this.className + " not found.");
            }
        }

        public void Delete(string Id)
        {
            T productToDelete = items.Find(i => i.ID == Id);

            if (productToDelete != null)
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

}

