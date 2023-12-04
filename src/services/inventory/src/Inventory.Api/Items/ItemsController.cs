using Common.Domain;
using MassTransit.Initializers;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Items;

[ApiController]
[Route("items")]
public class ItemsController : ControllerBase
{
    private readonly IRepository<InventoryItem> _inventoryItems;
    private readonly IRepository<CatalogItem> _catalogItems;

    public ItemsController (IRepository<InventoryItem> inventoryItems, IRepository<CatalogItem> catalogItems)
    {
        _inventoryItems    = inventoryItems;
        _catalogItems = catalogItems;
    }

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<IEnumerable<InventoryItemResponse>>> GetAsync(Guid userId)
    {
        if (userId == Guid.Empty) return BadRequest ();

        var inventoryItems = await _inventoryItems.GetAllAsync(item => item.UserId == userId);
        var catalogItemIds = inventoryItems.Select (item => item.CatalogItemId);
        var catalogItems   = await _catalogItems.GetAllAsync (item => catalogItemIds.Contains (item.Id));

        var inventoryItemResponses = inventoryItems.Select (inventoryItem =>
        {
            var catalogItem = catalogItems.Single (item => item.Id == inventoryItem.CatalogItemId);
            return inventoryItem.AsDto (catalogItem.Name, catalogItem.Description);
        });

        return Ok(inventoryItemResponses);
    }

    [HttpPost]
    public async Task<ActionResult<InventoryItemResponse>> PostAsync(GrantItemsRequest request)
    {
        var inventoryItem = await _inventoryItems.GetAsync (item => item.UserId == request.UserId && item.CatalogItemId == request.CatalogItemId);

        if (inventoryItem == null)
        {
            inventoryItem = InventoryItem.Create (request.UserId, request.CatalogItemId, request.Quantity, DateTimeOffset.UtcNow);
            await _inventoryItems.CreateAsync (inventoryItem);
        }
        else
        {
            inventoryItem.AddQuantity (request.Quantity);
            await _inventoryItems.UpdateAsync (inventoryItem);
        }

        return Ok ();
    }
}
