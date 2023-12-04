namespace Catalog.Contracts;

public record CatalogItemCreated(Guid CatalogItemId, string Name, string Description);
public record CatalogItemUpdated(Guid CatalogItemId, string Name, string Description);
public record CatalogItemDeleted(Guid CatalogItemId);
