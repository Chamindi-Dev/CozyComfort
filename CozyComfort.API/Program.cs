using CozyComfort.Infrastructure.Repositories;
using CozyComfort.Infrastructure.Interfaces;
using CozyComfort.Application.Services;
using CozyComfort.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;
using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.Entities;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
    options.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
});

builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IBlanketModelRepository, BlanketModelRepository>();
builder.Services.AddScoped<IDistributorRepository, DistributorRepository>();
builder.Services.AddScoped<ISellerRepository, SellerRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IFactoryInventoryRepository, FactoryInventoryRepository>();
builder.Services.AddScoped<IDistributorInventoryRepository, DistributorInventoryRepository>();
builder.Services.AddScoped<ISellerInventoryRepository, SellerInventoryRepository>();
builder.Services.AddScoped<IProductionCapacityRepository, ProductionCapacityRepository>();
builder.Services.AddScoped<ICustomerOrderRepository, CustomerOrderRepository>();
builder.Services.AddScoped<ICustomerOrderItemRepository, CustomerOrderItemRepository>();
builder.Services.AddScoped<IAvailabilityRequestRepository, AvailabilityRequestRepository>();
builder.Services.AddScoped<ITransferOrderRepository, TransferOrderRepository>();
builder.Services.AddScoped<ITransferOrderItemRepository, TransferOrderItemRepository>();
builder.Services.AddScoped<IStockMovementRepository, StockMovementRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<IBlanketModelService, BlanketModelService>();
builder.Services.AddScoped<IDistributorService, DistributorService>();
builder.Services.AddScoped<ISellerService, SellerService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IFactoryInventoryService, FactoryInventoryService>();
builder.Services.AddScoped<IDistributorInventoryService, DistributorInventoryService>();
builder.Services.AddScoped<ISellerInventoryService, SellerInventoryService>();
builder.Services.AddScoped<IProductionCapacityService, ProductionCapacityService>();
builder.Services.AddScoped<ICustomerOrderService, CustomerOrderService>();
builder.Services.AddScoped<ICustomerOrderItemService, CustomerOrderItemService>();
builder.Services.AddScoped<IAvailabilityRequestService, AvailabilityRequestService>();
builder.Services.AddScoped<ITransferOrderService, TransferOrderService>();
builder.Services.AddScoped<ITransferOrderItemService, TransferOrderItemService>();
builder.Services.AddScoped<IStockMovementService, StockMovementService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

var keyStr = builder.Configuration["Jwt:Key"] ?? "CozyComfortSuperSecretSecurityKeyThatIsAtLeast32BytesLong!";
var issuer = builder.Configuration["Jwt:Issuer"] ?? "CozyComfort";
var audience = builder.Configuration["Jwt:Audience"] ?? "CozyComfort";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr))
    };
});

// Configure CORS to allow access from local HTML frontend
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers(options =>
    {
        var policy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
        options.Filters.Add(new Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter(policy));
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.MaxDepth = 128;
    });


// Register OpenAPI document generation
// UseSystemTextJsonOptions() shares ReferenceHandler.IgnoreCycles with the schema generator,
// preventing stack overflows caused by circular navigation properties on EF entities.
builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Title = "Cozy Comfort API";
        document.Info.Version = "v1";
        
        var scheme = new Microsoft.OpenApi.OpenApiSecurityScheme
        {
            Type = Microsoft.OpenApi.SecuritySchemeType.Http,
            Name = "Authorization",
            In = Microsoft.OpenApi.ParameterLocation.Header,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "Input your JWT token in the format: Bearer {token}"
        };
        
        document.Components ??= new Microsoft.OpenApi.OpenApiComponents();
        document.Components.SecuritySchemes.Add("Bearer", scheme);
        
        document.Security ??= new List<Microsoft.OpenApi.OpenApiSecurityRequirement>();
        document.Security.Add(new Microsoft.OpenApi.OpenApiSecurityRequirement
        {
            [new Microsoft.OpenApi.OpenApiSecurityScheme { Reference = new Microsoft.OpenApi.OpenApiReference { Id = "Bearer", Type = Microsoft.OpenApi.ReferenceType.SecurityScheme } }] = new List<string>()
        });
        
        return Task.CompletedTask;
    });
});


var app = builder.Build();

// Run migrations and seed data at startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        Console.WriteLine("Applying database migrations...");
        context.Database.Migrate();
        Console.WriteLine("Database migration complete. Seeding data...");
        DbSeeder.SeedData(context);
        Console.WriteLine("Database seeding complete.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during DB startup: {ex.Message}");
    }
}

var serviceRole = Environment.GetEnvironmentVariable("SERVICE_ROLE") ?? "All-in-One";
Console.WriteLine($"=========================================");
Console.WriteLine($"Starting Cozy Comfort Service: {serviceRole}");
Console.WriteLine($"=========================================");

// Always expose OpenAPI JSON and Scalar UI (accessible in all environments for demo purposes)
app.MapOpenApi("/openapi/v1.json");

app.MapScalarApiReference("/scalar", options =>
{
    options.Title = $"Cozy Comfort API – {serviceRole}";
    options.Theme = ScalarTheme.Purple;
    options.WithOpenApiRoutePattern("/openapi/v1.json");
    options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
});

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// Fake Policy Evaluator to bypass Auth checks locally
public class FakePolicyEvaluator : IPolicyEvaluator
{
    public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
    {
        var principal = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "DevUser"),
            new Claim(ClaimTypes.Role, "Admin")
        }, "FakeScheme"));

        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, "FakeScheme")));
    }

    public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object? resource)
    {
        return Task.FromResult(PolicyAuthorizationResult.Success());
    }
}

// Data Seeding Utility
public static class DbSeeder
{
    public static void SeedData(ApplicationDbContext context)
    {
        if (!context.Roles.Any())
        {
            var roles = new List<Role>
            {
                new Role { RoleName = "Admin" },
                new Role { RoleName = "Distributor" },
                new Role { RoleName = "Seller" },
                new Role { RoleName = "Manufacturer" }
            };
            context.Roles.AddRange(roles);
            context.SaveChanges();
        }

        if (!context.Users.Any())
        {
            var adminRole = context.Roles.First(r => r.RoleName == "Admin");
            var distRole = context.Roles.First(r => r.RoleName == "Distributor");
            var sellerRole = context.Roles.First(r => r.RoleName == "Seller");
            var mfrRole = context.Roles.First(r => r.RoleName == "Manufacturer");

            var users = new List<User>
            {
                new User { FullName = "Admin User", Email = "admin@comfort.com", Password = "", RoleId = adminRole.Id },
                new User { FullName = "Distributor User", Email = "distributor@comfort.com", Password = "", RoleId = distRole.Id },
                new User { FullName = "Seller User", Email = "seller@comfort.com", Password = "", RoleId = sellerRole.Id },
                new User { FullName = "Manufacturer User", Email = "manufacturer@comfort.com", Password = "", RoleId = mfrRole.Id }
            };

            foreach (var u in users)
            {
                u.Password = CozyComfort.Application.Services.PasswordHasher.HashPassword(u.Email.Split('@')[0] + "@123");
            }

            context.Users.AddRange(users);
            context.SaveChanges();
        }

        if (!context.Materials.Any())
        {
            var materials = new List<Material>
            {
                new Material { MaterialName = "Premium Fleece", Description = "Ultra-soft and lightweight polyester synthetic wool." },
                new Material { MaterialName = "Organic Cotton", Description = "100% certified organic breathable cotton." },
                new Material { MaterialName = "Sherpa Wool", Description = "Thick, warm wool resembling sheep's fleece." }
            };
            context.Materials.AddRange(materials);
            context.SaveChanges();
        }

        if (!context.BlanketModels.Any())
        {
            var fleece = context.Materials.First(m => m.MaterialName == "Premium Fleece");
            var cotton = context.Materials.First(m => m.MaterialName == "Organic Cotton");
            var sherpa = context.Materials.First(m => m.MaterialName == "Sherpa Wool");

            var models = new List<BlanketModel>
            {
                new BlanketModel { SKU = "BLK-FLE-001", ModelName = "Velvet Fleece Cozy", MaterialId = fleece.Id, Size = "Queen", Color = "Royal Blue", UnitPrice = 45.00m },
                new BlanketModel { SKU = "BLK-FLE-002", ModelName = "Thermal Fleece Shield", MaterialId = fleece.Id, Size = "King", Color = "Charcoal Gray", UnitPrice = 55.00m },
                new BlanketModel { SKU = "BLK-COT-001", ModelName = "Breathable Cotton Grid", MaterialId = cotton.Id, Size = "Double", Color = "Off-White", UnitPrice = 60.00m },
                new BlanketModel { SKU = "BLK-COT-002", ModelName = "Summer Breeze Cotton", MaterialId = cotton.Id, Size = "Single", Color = "Sage Green", UnitPrice = 40.00m },
                new BlanketModel { SKU = "BLK-SHR-001", ModelName = "Fluffy Sherpa Dream", MaterialId = sherpa.Id, Size = "Queen", Color = "Cream White", UnitPrice = 75.00m }
            };
            context.BlanketModels.AddRange(models);
            context.SaveChanges();
        }

        if (!context.Distributors.Any())
        {
            var distributors = new List<Distributor>
            {
                new Distributor { DistributorName = "Comfort Logistics Ltd", Email = "logistics@comfort.com", Phone = "011-2345678", Address = "100 Warehouse Way, Colombo", ServiceArea = "Western Province" },
                new Distributor { DistributorName = "Apex Distributors", Email = "orders@apexdist.com", Phone = "033-9876543", Address = "45 Distribution Rd, Gampaha", ServiceArea = "Sabaragamuwa Province" }
            };
            context.Distributors.AddRange(distributors);
            context.SaveChanges();
        }

        if (!context.Sellers.Any())
        {
            var dist1 = context.Distributors.First();
            var dist2 = context.Distributors.Skip(1).First();

            var sellers = new List<Seller>
            {
                new Seller { SellerName = "Bed Bath & Cozy", StoreType = "Physical", Email = "contact@bedbathcozy.lk", Phone = "011-5551234", Address = "45 Galle Road, Colombo 03", DistributorId = dist1.Id },
                new Seller { SellerName = "SoftSheets Online", StoreType = "Online", Email = "support@softsheets.lk", Phone = "077-1234567", Address = "Virtual Store, Sri Lanka", DistributorId = dist2.Id }
            };
            context.Sellers.AddRange(sellers);
            context.SaveChanges();
        }

        if (!context.Customers.Any())
        {
            var customers = new List<Customer>
            {
                new Customer { CustomerName = "John Doe", Email = "john@example.com", Phone = "071-2223334", Address = "12 Main St, Kandy" },
                new Customer { CustomerName = "Jane Smith", Email = "jane@example.com", Phone = "072-4445556", Address = "78 Park Lane, Negombo" }
            };
            context.Customers.AddRange(customers);
            context.SaveChanges();
        }

        if (!context.FactoryInventories.Any())
        {
            var models = context.BlanketModels.ToList();
            var factoryInventory = models.Select(m => new FactoryInventory
            {
                BlanketModelId = m.Id,
                QuantityOnHand = 150,
                ReservedQuantity = 0,
                LastUpdated = DateTime.Now
            }).ToList();
            context.FactoryInventories.AddRange(factoryInventory);
            context.SaveChanges();
        }

        if (!context.DistributorInventories.Any())
        {
            var dists = context.Distributors.ToList();
            var models = context.BlanketModels.ToList();
            var distInventory = new List<DistributorInventory>();
            foreach (var dist in dists)
            {
                foreach (var model in models)
                {
                    distInventory.Add(new DistributorInventory
                    {
                        DistributorId = dist.Id,
                        BlanketModelId = model.Id,
                        QuantityOnHand = 30,
                        ReservedQuantity = 0,
                        LastUpdated = DateTime.Now
                    });
                }
            }
            context.DistributorInventories.AddRange(distInventory);
            context.SaveChanges();
        }

        if (!context.SellerInventories.Any())
        {
            var sellers = context.Sellers.ToList();
            var models = context.BlanketModels.ToList();
            var sellerInventory = new List<SellerInventory>();
            foreach (var seller in sellers)
            {
                foreach (var model in models)
                {
                    sellerInventory.Add(new SellerInventory
                    {
                        SellerId = seller.Id,
                        BlanketModelId = model.Id,
                        QuantityOnHand = 5,
                        ReservedQuantity = 0,
                        LastUpdated = DateTime.Now
                    });
                }
            }
            context.SellerInventories.AddRange(sellerInventory);
            context.SaveChanges();
        }

        if (!context.ProductionCapacities.Any())
        {
            var models = context.BlanketModels.ToList();
            var capacities = models.Select(m => new ProductionCapacity
            {
                BlanketModelId = m.Id,
                DailyCapacity = 25,
                WeeklyCapacity = 175,
                CurrentPendingQuantity = 0,
                LeadTimeDays = 3,
                LastUpdated = DateTime.Now
            }).ToList();
            context.ProductionCapacities.AddRange(capacities);
            context.SaveChanges();
        }
    }
}