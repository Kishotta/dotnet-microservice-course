using Catalog.Domain.Items;
using Common.MongoDb;
using Common.RabbitMq;

var builder = WebApplication.CreateBuilder (args);

builder.Services
       .AddMongoDb (builder.Configuration)
       .AddMongoDbRepository<Item> ("items")
       .AddMassTransitWithRabbitMq (builder.Configuration);

builder.Services
       .AddControllers (options => options.SuppressAsyncSuffixInActionNames = false);

builder.Services
       .AddEndpointsApiExplorer ()
       .AddSwaggerGen ();

var app = builder.Build ();

if (app.Environment.IsDevelopment ())
{
    app.UseSwagger ();
    app.UseSwaggerUI ();
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
app.UseCors ();

app.Run ();
