using Api.Domain;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService itemsService;

        public ItemsController(IItemsService itemsService)
        {
            this.itemsService = itemsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> Get()
        {
            try
            {
                var itemDtos = await itemsService.GetItems();

                return Ok(itemDtos);
            }
            catch (HttpRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<ItemDto>>> Post([FromForm]string[] items)
        {
            if(items == null || items.Length == 0)
            {
                return BadRequest("No items selected");
            }

            try
            {
                var itemDtos = await itemsService.GetItems(items);

                return Ok(itemDtos);
            }
            catch (HttpRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}