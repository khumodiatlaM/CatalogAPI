using CatalogAPI.Data;
using CatalogAPI.Dtos;
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
        public IEnumerable<ItemDto> GetItems()
        {
            var items = data.GetItems().Select(item => item.AsDto());

            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = data.GetItem(id);

            if(item is null)
            {
                return NotFound();
            }
            return Ok(item.AsDto());
        }

        [HttpPost]
        public ActionResult<CreateItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new(){

                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            data.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());

        }

        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id ,UpdateItemDto itemDto)
        {
            var item = data.GetItem(id);

            if(item is null)
            {
                return NotFound();
            }

            Item updatedItem = item with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            data.UpdateItem(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var item = GetItem(id);

            if(item is null)
            {
                return NotFound();
            }

            data.DeleteItem(id);

            return NoContent();
        }
    }
}
