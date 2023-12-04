using Catalog.Contracts;
using Common.Domain;
using Inventory.Api.Items;
using MassTransit;

namespace Inventory.Api.Consumers;

public class CatalogItemCreatedConsumer : IConsumer<CatalogItemCreated>
{
    private readonly IRepository<CatalogItem> _catalogItems;

    public CatalogItemCreatedConsumer (IRepository<CatalogItem> catalogItems)
    {
        _catalogItems = catalogItems;
    }

    public async Task Consume (ConsumeContext<CatalogItemCreated> context)
    {
        var message = context.Message;

        var item = await _catalogItems.GetAsync (message.CatalogItemId);

        if (item is not null) return;

        var catalogItem = CatalogItem.Create (message.CatalogItemId, message.Name, message.Description);

        await _catalogItems.CreateAsync (catalogItem);
    }
}
