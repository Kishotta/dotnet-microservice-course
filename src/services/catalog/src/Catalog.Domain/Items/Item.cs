using Common.Domain;

namespace Catalog.Domain.Items;

public class Item : IEntity
{
    public Guid Id { get; private init; }

    public string  Name        { get; private set; } = string.Empty;
    public string  Description { get; private set; } = string.Empty;
    public decimal Price       { get; private set; }

    public DateTimeOffset CreatedDate { get; private init; }

    public static Item Create (string name, string description, decimal price)
    {
        return new Item
        {
            Id = Guid.NewGuid (),
            Name = name,
            Description = description,
            Price = price,
            CreatedDate = DateTimeOffset.UtcNow
        };
    }

    public void Update (string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
    }
}
