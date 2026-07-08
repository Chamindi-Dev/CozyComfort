using CozyComfort.API.Repositories;
using CozyComfort.Application.Interfaces;
using CozyComfort.Application.Repositories;
using CozyComfort.Application.Services;
using CozyComfort.Data;
using CozyComfort.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();

builder.Services.AddScoped<IBlanketModelRepository, BlanketModelRepository>();

builder.Services.AddScoped<IDistributorRepository, DistributorRepository>();

builder.Services.AddScoped<ISellerRepository, SellerRepository>();
builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<IBlanketModelService, BlanketModelService>();
builder.Services.AddScoped<IDistributorService, DistributorService>();
builder.Services.AddScoped<ISellerService, SellerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IFactoryInventoryRepository, FactoryInventoryRepository>();
builder.Services.AddScoped<IFactoryInventoryService, FactoryInventoryService>();
builder.Services.AddScoped<IDistributorInventoryRepository, DistributorInventoryRepository>();
builder.Services.AddScoped<IDistributorInventoryService, DistributorInventoryService>();
builder.Services.AddScoped<ISellerInventoryRepository, SellerInventoryRepository>();
builder.Services.AddScoped<ISellerInventoryService, SellerInventoryService>();
builder.Services.AddScoped<IProductionCapacityRepository, ProductionCapacityRepository>();
builder.Services.AddScoped<IProductionCapacityService, ProductionCapacityService>();
builder.Services.AddScoped<ICustomerOrderRepository, CustomerOrderRepository>();
builder.Services.AddScoped<ICustomerOrderService, CustomerOrderService>();
builder.Services.AddScoped<ICustomerOrderItemRepository, CustomerOrderItemRepository>();
builder.Services.AddScoped<ICustomerOrderItemService, CustomerOrderItemService>();
builder.Services.AddScoped<IAvailabilityRequestRepository, AvailabilityRequestRepository>();
builder.Services.AddScoped<IAvailabilityRequestService, AvailabilityRequestService>();
builder.Services.AddScoped<ITransferOrderRepository, TransferOrderRepository>();
builder.Services.AddScoped<ITransferOrderService, TransferOrderService>();
builder.Services.AddScoped<ITransferOrderItemRepository, TransferOrderItemRepository>();
builder.Services.AddScoped<ITransferOrderItemService, TransferOrderItemService>();
builder.Services.AddScoped<IStockMovementRepository, StockMovementRepository>();
builder.Services.AddScoped<IStockMovementService, StockMovementService>();



builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

                                                                                                    

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


app.UseAuthorization();


app.MapControllers();


app.Run();