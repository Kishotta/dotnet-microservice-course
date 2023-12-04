using Common.MongoDb;
using Common.RabbitMq;
using Inventory.Api.Items;

var builder = WebApplication.CreateBuilder(args);

builder.Services
       .AddMongoDb (builder.Configuration)
       .AddMongoDbRepository<InventoryItem> ("inventoryitems")
       .AddMongoDbRepository<CatalogItem> ("catalogitems");

builder.Services
       .AddMassTransitWithRabbitMq (builder.Configuration);

builder.Services.AddControllers (options => options.SuppressAsyncSuffixInActionNames = false);

builder.Services.AddEndpointsApiExplorer ();
builder.Services.AddSwaggerGen ();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors (corsPolicyBuilder =>
    {
           corsPolicyBuilder.AllowAnyOrigin()
                            .AllowAnyHeader ()
                            .AllowAnyMethod ();
    });
}

app.UseHttpsRedirection ();
app.UseAuthorization ();
app.MapControllers ();

app.Run();
