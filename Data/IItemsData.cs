using CatalogAPI.Models;
using System;
using System.Collections.Generic;

namespace CatalogAPI.Data
{
    public interface IItemsData
    {
        Item GetItem(Guid id);

        IEnumerable<Item> GetItems();

        void CreateItem(Item item);

        void UpdateItem(Item item);

        void DeleteItem(Guid id);
    }
}