using Common.Domain;

namespace Inventory.Api.Items;

public class CatalogItem : IEntity
{
    public Guid Id { get; private init;  }

    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public static CatalogItem Create (Guid catalogItemId, string name, string description)
    {
        return new CatalogItem
        {
            Id          = catalogItemId,
            Name        = name,
            Description = description
        };
    }

    public void Update (string name, string description)
    {
        Name        = name;
        Description = description;
    }
}
