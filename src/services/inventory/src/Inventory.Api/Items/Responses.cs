namespace Inventory.Api.Items;

public record InventoryItemResponse(Guid CatalogItemId, string Name, string Description, int Quantity, DateTimeOffset AcquiredDate);
public record CatalogItemResponse(Guid Id, string Name, string Description);
