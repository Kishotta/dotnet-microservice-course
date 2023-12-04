using Catalog.Contracts;
using Common.Domain;
using Inventory.Api.Items;
using MassTransit;

namespace Inventory.Api.Consumers;

public class CatalogItemDeletedConsumer : IConsumer<CatalogItemDeleted>
{
    private readonly IRepository<CatalogItem> _catalogItems;

    public CatalogItemDeletedConsumer (IRepository<CatalogItem> catalogItems)
    {
        _catalogItems = catalogItems;
    }

    public async Task Consume (ConsumeContext<CatalogItemDeleted> context)
    {
        var message = context.Message;

        var item = await _catalogItems.GetAsync (message.CatalogItemId);

        if (item is null) return;

        await _catalogItems.DeleteAsync (message.CatalogItemId);
    }
}
