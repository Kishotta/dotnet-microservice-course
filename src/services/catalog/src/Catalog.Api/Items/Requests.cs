using System.ComponentModel.DataAnnotations;

namespace Catalog.Api.Items;

public record CreateItemRequest([Required] string Name, string Description, [Range(0, 1000)] decimal Price);

public record UpdateItemRequest([Required] string Name, string Description, [Range(0, 1000)] decimal Price);
