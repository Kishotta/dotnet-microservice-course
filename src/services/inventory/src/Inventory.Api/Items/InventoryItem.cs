using Common.Domain;

namespace Inventory.Api.Items;

public class InventoryItem : IEntity
{
    public Guid Id { get; private init;  }

    public Guid UserId        { get; private set; }
    public Guid CatalogItemId { get; private set; }
    public int  Quantity      { get; private set; }

    public DateTimeOffset AcquiredDate { get; private set; }

    public static InventoryItem Create (Guid userId, Guid catalogItemId, int quantity, DateTimeOffset acquiredDate)
    {
        return new InventoryItem
        {
            Id             = Guid.NewGuid(),
            UserId         = userId,
            CatalogItemId  = catalogItemId,
            Quantity       = quantity,
            AcquiredDate   = acquiredDate
        };
    }

    public void AddQuantity (int quantity)
    {
        Quantity += quantity;
    }
}
