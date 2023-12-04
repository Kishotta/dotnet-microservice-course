namespace Inventory.Api.Items;

public static class Extensions
{
    public static InventoryItemResponse AsDto(this InventoryItem item, string name, string description)
    {
        return new InventoryItemResponse(item.CatalogItemId, name, description, item.Quantity, item.AcquiredDate);
    }
}
