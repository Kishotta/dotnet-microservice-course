using Catalog.Contracts;
using Catalog.Domain.Items;
using Common.Domain;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Items;

[ApiController]
[Route("items")]
public class ItemsController : ControllerBase
{
    private readonly IRepository<Item> _items;
    private readonly IPublishEndpoint _publishEndpoint;

    public ItemsController (IRepository<Item> items, IPublishEndpoint publishEndpoint)
    {
        _items           = items;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<IEnumerable<ItemResponse>> GetAsync()
    {
        var items = await _items.GetAllAsync ();

        return items.Select (item => item.AsDto ());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ItemResponse>> GetByIdAsync(Guid id)
    {
        var item = await _items.GetAsync (id);

        if (item is null) return NotFound();

        return item.AsDto();
    }

    [HttpPost]
    public async Task<ActionResult<ItemResponse>> PostAsync(CreateItemRequest request)
    {
        var item = Item.Create (request.Name, request.Description, request.Price);

        await _items.CreateAsync (item);
        await _publishEndpoint.Publish (new CatalogItemCreated (item.Id, item.Name, item.Description));

        return CreatedAtAction(nameof(GetByIdAsync), new {id = item.Id}, item);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> PutAsync(Guid id, UpdateItemRequest request)
    {
        var item = await _items.GetAsync (id);

        if (item is null) return NotFound();

        item.Update (request.Name, request.Description, request.Price);

        await _items.UpdateAsync (item);
        await _publishEndpoint.Publish (new CatalogItemUpdated (item.Id, item.Name, item.Description));

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        var item = await _items.GetAsync (id);

        if (item is null) return NotFound();

        await _items.DeleteAsync (id);
        await _publishEndpoint.Publish (new CatalogItemDeleted (id));

        return NoContent();
    }
}
