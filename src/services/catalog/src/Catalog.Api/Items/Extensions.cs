using Catalog.Domain.Items;

namespace Catalog.Api.Items;

public static class Extensions
{
    public static ItemResponse AsDto(this Item item)
    {
        return new ItemResponse(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
    }
}
