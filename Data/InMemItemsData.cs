using CatalogAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogAPI.Data
{
    public class InMemItemsData : IItemsData
    {
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow }

        };

        public IEnumerable<Item> GetItems() => items;

        public Item GetItem(Guid id)
        {
            return items.SingleOrDefault(item => item.Id == id);
        }

        public void CreateItem(Item item)
        {
            items.Add(item); 
        }

        public void UpdateItem(Item item)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);

            items[index] = item;
        }

        public void DeleteItem(Guid id)
        {
            var index = items.FindIndex(item => item.Id == id);
            items.RemoveAt(index);
        }
    }
}
