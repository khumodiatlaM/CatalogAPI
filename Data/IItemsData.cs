using CatalogAPI.Models;
using System;
using System.Collections.Generic;

namespace CatalogAPI.Data
{
    public interface IItemsData
    {
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();
    }
}