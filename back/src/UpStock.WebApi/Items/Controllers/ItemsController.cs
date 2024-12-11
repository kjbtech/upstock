using Microsoft.AspNetCore.Mvc;
using UpStock.Domain.Files;
using UpStock.Domain.Items;
using UpStock.Kernel;

namespace UpStock.WebApi.Items.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IQueryHandler<ListItems, ItemsList> _listItems;

    public ItemsController(IQueryHandler<ListItems, ItemsList> listItems)
    {
        _listItems = listItems;
    }

    [HttpGet]
    public async Task<ActionResult<ListItems>> List(
        [FromQuery] PaginationParameter paginationParameter)
    {
        var invoices = await _listItems.HandleAsync(new ListItems(paginationParameter));

        return Ok(invoices);
    }
}
