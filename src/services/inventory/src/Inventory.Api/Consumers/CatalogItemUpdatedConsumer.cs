using Catalog.Contracts;
using Common.Domain;
using Inventory.Api.Items;
using MassTransit;

namespace Inventory.Api.Consumers;

public class CatalogItemUpdatedConsumer : IConsumer<CatalogItemUpdated>
{
    private readonly IRepository<CatalogItem> _catalogItems;

    public CatalogItemUpdatedConsumer (IRepository<CatalogItem> catalogItems)
    {
        _catalogItems = catalogItems;
    }

    public async Task Consume (ConsumeContext<CatalogItemUpdated> context)
    {
        var message = context.Message;

        var item = await _catalogItems.GetAsync (message.CatalogItemId);

        if (item is null)
        {
            var catalogItem = CatalogItem.Create (message.CatalogItemId, message.Name, message.Description);

            await _catalogItems.CreateAsync (catalogItem);
        }
        else
        {
            item.Update(message.Name, message.Description);

            await _catalogItems.UpdateAsync (item);
        }
    }
}
