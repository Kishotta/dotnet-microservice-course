namespace Inventory.Api.Items;

public record GrantItemsRequest(Guid UserId, Guid CatalogItemId, int Quantity);
