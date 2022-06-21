using CatalogAPI.Data;
using CatalogAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsData data;

        public ItemsController(IItemsData data)
        {
            this.data =  data;
        }


        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            var items = data.GetItems();
            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(Guid id)
        {
            var item = data.GetItem(id);

            if(item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }
    }
}
