namespace Catalog.Api.Items;

public record ItemResponse(Guid Id, string Name, string Description, decimal Price, DateTimeOffset CreatedDate);
