using CozyComfort.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CozyComfort.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Material> Materials { get; set; }
        public DbSet<BlanketModel> BlanketModels { get; set; }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FactoryInventory> FactoryInventories { get; set; }
        public DbSet<DistributorInventory> DistributorInventories { get; set; }
        public DbSet<SellerInventory> SellerInventories { get; set; }
        public DbSet<ProductionCapacity> ProductionCapacities { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<CustomerOrderItem> CustomerOrderItems { get; set; }
        public DbSet<AvailabilityRequest> AvailabilityRequests { get; set; }
        public DbSet<TransferOrder> TransferOrders { get; set; }
        public DbSet<TransferOrderItem> TransferOrderItems { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Material>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Material>()
                .Property(m => m.MaterialName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Material>()
                .HasIndex(m => m.MaterialName)
                .IsUnique();

            modelBuilder.Entity<BlanketModel>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<BlanketModel>()
                .Property(b => b.SKU)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<BlanketModel>()
                .HasIndex(b => b.SKU)
                .IsUnique();

            modelBuilder.Entity<BlanketModel>()
                .Property(b => b.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<BlanketModel>()
                .Property(b => b.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<BlanketModel>()
                .HasOne(b => b.Material)
                .WithMany(m => m.BlanketModels)
                .HasForeignKey(b => b.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Distributor>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Distributor>()
                .Property(d => d.DistributorName)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Distributor>()
                .Property(d => d.Email)
                .HasMaxLength(150);

            modelBuilder.Entity<Distributor>()
                .Property(d => d.Phone)
                .HasMaxLength(30);

            modelBuilder.Entity<Distributor>()
                .Property(d => d.Address)
                .HasMaxLength(255);

            modelBuilder.Entity<Distributor>()
                .Property(d => d.ServiceArea)
                .HasMaxLength(150);

            modelBuilder.Entity<Distributor>()
                .Property(d => d.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Seller>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Seller>()
                .Property(s => s.SellerName)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Seller>()
                .Property(s => s.StoreType)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Seller>()
                .Property(s => s.Email)
                .HasMaxLength(150);

            modelBuilder.Entity<Seller>()
                .Property(s => s.Phone)
                .HasMaxLength(30);

            modelBuilder.Entity<Seller>()
                .Property(s => s.Address)
                .HasMaxLength(255);

            modelBuilder.Entity<Seller>()
                .Property(s => s.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Seller>()
                .HasOne(s => s.Distributor)
                .WithMany(d => d.Sellers)
                .HasForeignKey(s => s.DistributorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Seller>()
                .ToTable(t => t.HasCheckConstraint(
                    "CK_Sellers_StoreType",
                    "[StoreType] IN ('Online','Physical','Hybrid')"));

            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Customer>()
                .Property(c => c.CustomerName)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Email)
                .HasMaxLength(150);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Phone)
                .HasMaxLength(30);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Address)
                .HasMaxLength(255);

            modelBuilder.Entity<FactoryInventory>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<FactoryInventory>()
                .HasIndex(f => f.BlanketModelId)
                .IsUnique();

            modelBuilder.Entity<FactoryInventory>()
                .Property(f => f.QuantityOnHand)
                .HasDefaultValue(0);

            modelBuilder.Entity<FactoryInventory>()
                .Property(f => f.ReservedQuantity)
                .HasDefaultValue(0);

            modelBuilder.Entity<FactoryInventory>()
                .Property(f => f.AvailableQuantity)
                .HasComputedColumnSql(
                    "[QuantityOnHand]-[ReservedQuantity]",
                    stored: true);

            modelBuilder.Entity<FactoryInventory>()
                .Property(f => f.LastUpdated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<FactoryInventory>()
                .ToTable(t => t.HasCheckConstraint(
                    "CK_FactoryInventories_Qty",
                    "[QuantityOnHand] >= 0 AND [ReservedQuantity] >= 0 AND [ReservedQuantity] <= [QuantityOnHand]"));

            modelBuilder.Entity<FactoryInventory>()
                .HasOne(f => f.BlanketModel)
                .WithMany(b => b.FactoryInventories)
                .HasForeignKey(f => f.BlanketModelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DistributorInventory>()
                .HasKey(di => di.Id);

            modelBuilder.Entity<DistributorInventory>()
                .HasIndex(di => new
                {
                    di.DistributorId,
                    di.BlanketModelId
                })
                .IsUnique();

            modelBuilder.Entity<DistributorInventory>()
                .Property(di => di.QuantityOnHand)
                .HasDefaultValue(0);

            modelBuilder.Entity<DistributorInventory>()
                .Property(di => di.ReservedQuantity)
                .HasDefaultValue(0);

            modelBuilder.Entity<DistributorInventory>()
                .Property(di => di.AvailableQuantity)
                .HasComputedColumnSql(
                    "[QuantityOnHand]-[ReservedQuantity]",
                    stored: true);

            modelBuilder.Entity<DistributorInventory>()
                .Property(di => di.LastUpdated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<DistributorInventory>()
                .ToTable(t => t.HasCheckConstraint(
                    "CK_DistributorInventories_Qty",
                    "[QuantityOnHand] >= 0 AND [ReservedQuantity] >= 0 AND [ReservedQuantity] <= [QuantityOnHand]"));

            modelBuilder.Entity<DistributorInventory>()
                .HasOne(di => di.Distributor)
                .WithMany(d => d.DistributorInventories)
                .HasForeignKey(di => di.DistributorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DistributorInventory>()
                .HasOne(di => di.BlanketModel)
                .WithMany(b => b.DistributorInventories)
                .HasForeignKey(di => di.BlanketModelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SellerInventory>()
                .HasKey(si => si.Id);

            modelBuilder.Entity<SellerInventory>()
                .HasIndex(si => new
                {
                    si.SellerId,
                    si.BlanketModelId
                })
                .IsUnique();

            modelBuilder.Entity<SellerInventory>()
                .Property(si => si.QuantityOnHand)
                .HasDefaultValue(0);

            modelBuilder.Entity<SellerInventory>()
                .Property(si => si.ReservedQuantity)
                .HasDefaultValue(0);

            modelBuilder.Entity<SellerInventory>()
                .Property(si => si.AvailableQuantity)
                .HasComputedColumnSql(
                    "[QuantityOnHand]-[ReservedQuantity]",
                    stored: true);

            modelBuilder.Entity<SellerInventory>()
                .Property(si => si.LastUpdated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<SellerInventory>()
                .ToTable(t => t.HasCheckConstraint(
                    "CK_SellerInventories_Qty",
                    "[QuantityOnHand] >= 0 AND [ReservedQuantity] >= 0 AND [ReservedQuantity] <= [QuantityOnHand]"));

            modelBuilder.Entity<SellerInventory>()
                .HasOne(si => si.Seller)
                .WithMany(s => s.SellerInventories)
                .HasForeignKey(si => si.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SellerInventory>()
                .HasOne(si => si.BlanketModel)
                .WithMany(b => b.SellerInventories)
                .HasForeignKey(si => si.BlanketModelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductionCapacity>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<ProductionCapacity>()
                .HasIndex(p => p.BlanketModelId)
                .IsUnique();

            modelBuilder.Entity<ProductionCapacity>()
                .Property(p => p.DailyCapacity)
                .IsRequired();

            modelBuilder.Entity<ProductionCapacity>()
                .Property(p => p.WeeklyCapacity)
                .IsRequired();

            modelBuilder.Entity<ProductionCapacity>()
                .Property(p => p.CurrentPendingQuantity)
                .HasDefaultValue(0);

            modelBuilder.Entity<ProductionCapacity>()
                .Property(p => p.LeadTimeDays)
                .IsRequired();

            modelBuilder.Entity<ProductionCapacity>()
                .Property(p => p.LastUpdated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<ProductionCapacity>()
                .ToTable(t =>
                    t.HasCheckConstraint(
                        "CK_ProductionCapacities_Values",
                        @"DailyCapacity >= 0
            AND WeeklyCapacity >= 0
            AND CurrentPendingQuantity >= 0
            AND LeadTimeDays >= 0"));

            modelBuilder.Entity<ProductionCapacity>()
                .HasOne(p => p.BlanketModel)
                .WithMany(b => b.ProductionCapacities)
                .HasForeignKey(p => p.BlanketModelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerOrder>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<CustomerOrder>()
                .Property(o => o.OrderNumber)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<CustomerOrder>()
                .HasIndex(o => o.OrderNumber)
                .IsUnique();

            modelBuilder.Entity<CustomerOrder>()
                .Property(o => o.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");

            modelBuilder.Entity<CustomerOrder>()
                .Property(o => o.FinalSource)
                .HasMaxLength(50);

            modelBuilder.Entity<CustomerOrder>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2)
                .HasDefaultValue(0);

            modelBuilder.Entity<CustomerOrder>()
                .Property(o => o.OrderDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<CustomerOrder>()
                .Property(o => o.Remarks)
                .HasMaxLength(500);

            modelBuilder.Entity<CustomerOrder>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.CustomerOrders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerOrder>()
                .HasOne(o => o.Seller)
                .WithMany(s => s.CustomerOrders)
                .HasForeignKey(o => o.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerOrder>()
                .ToTable(t =>
                {
                    t.HasCheckConstraint(
                        "CK_CustomerOrders_Status",
                        @"Status IN
            (
            'Pending',
            'CheckingSellerStock',
            'CheckingDistributorStock',
            'CheckingFactoryStock',
            'WaitingProduction',
            'Confirmed',
            'InFulfillment',
            'Dispatched',
            'Delivered',
            'Cancelled'
            )");

                    t.HasCheckConstraint(
                        "CK_CustomerOrders_FinalSource",
                        @"FinalSource IS NULL OR FinalSource IN
            (
            'SellerStock',
            'DistributorStock',
            'FactoryStock',
            'FactoryProduction'
            )");
                });

            modelBuilder.Entity<CustomerOrderItem>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<CustomerOrderItem>()
                .Property(x => x.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CustomerOrderItem>()
                .Property(x => x.Subtotal)
                .HasPrecision(18, 2)
                .HasComputedColumnSql(
                    "[Quantity] * [UnitPrice]",
                    stored: true);

            modelBuilder.Entity<CustomerOrderItem>()
                .HasOne(x => x.CustomerOrder)
                .WithMany(x => x.CustomerOrderItems)
                .HasForeignKey(x => x.CustomerOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CustomerOrderItem>()
                .HasOne(x => x.BlanketModel)
                .WithMany(x => x.CustomerOrderItems)
                .HasForeignKey(x => x.BlanketModelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerOrderItem>()
                .ToTable(t =>
                    t.HasCheckConstraint(
                        "CK_CustomerOrderItems_Values",
                        "[Quantity] > 0 AND [UnitPrice] >= 0"
                    ));

            modelBuilder.Entity<AvailabilityRequest>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<AvailabilityRequest>()
                .Property(x => x.RequestNumber)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<AvailabilityRequest>()
                .HasIndex(x => x.RequestNumber)
                .IsUnique();

            modelBuilder.Entity<AvailabilityRequest>()
                .Property(x => x.Status)
                .HasDefaultValue("Pending");

            modelBuilder.Entity<AvailabilityRequest>()
                .Property(x => x.RequestedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<AvailabilityRequest>()
                .HasOne(x => x.CustomerOrder)
                .WithMany(x => x.AvailabilityRequests)
                .HasForeignKey(x => x.CustomerOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AvailabilityRequest>()
                .HasOne(x => x.BlanketModel)
                .WithMany(x => x.AvailabilityRequests)
                .HasForeignKey(x => x.BlanketModelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AvailabilityRequest>()
                .HasOne(x => x.Seller)
                .WithMany()
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AvailabilityRequest>()
                .HasOne(x => x.Distributor)
                .WithMany()
                .HasForeignKey(x => x.DistributorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AvailabilityRequest>()
                .ToTable(t =>
                {
                    t.HasCheckConstraint(
                        "CK_AvailabilityRequests_Level",
                        "[RequestLevel] IN ('SellerToDistributor','DistributorToFactory')"
                    );

                    t.HasCheckConstraint(
                        "CK_AvailabilityRequests_Status",
                        "[Status] IN ('Pending','Available','NotAvailable','ProductionPossible','Rejected')"
                    );

                    t.HasCheckConstraint(
                        "CK_AvailabilityRequests_Qty",
                        "[RequestedQuantity] > 0"
                    );
                });

            modelBuilder.Entity<TransferOrder>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<TransferOrder>()
                .HasIndex(x => x.TransferNumber)
                .IsUnique();

            modelBuilder.Entity<TransferOrder>()
                .Property(x => x.RequestedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<TransferOrder>()
                .Property(x => x.Status)
                .HasDefaultValue("Pending");

            modelBuilder.Entity<TransferOrder>()
                .HasOne(x => x.CustomerOrder)
                .WithMany()
                .HasForeignKey(x => x.CustomerOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransferOrder>()
                .HasOne(x => x.FromDistributor)
                .WithMany()
                .HasForeignKey(x => x.FromDistributorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransferOrder>()
                .HasOne(x => x.ToDistributor)
                .WithMany()
                .HasForeignKey(x => x.ToDistributorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransferOrder>()
                .HasOne(x => x.FromSeller)
                .WithMany()
                .HasForeignKey(x => x.FromSellerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransferOrder>()
                .HasOne(x => x.ToSeller)
                .WithMany()
                .HasForeignKey(x => x.ToSellerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransferOrder>()
                .ToTable(t =>
                {
                    t.HasCheckConstraint(
                        "CK_TransferOrders_FromLocationType",
                        "[FromLocationType] IN ('Factory','Distributor','Seller')"
                    );

                    t.HasCheckConstraint(
                        "CK_TransferOrders_ToLocationType",
                        "[ToLocationType] IN ('Distributor','Seller','Customer')"
                    );

                    t.HasCheckConstraint(
                        "CK_TransferOrders_Status",
                        "[Status] IN ('Pending','Approved','InTransit','Completed','Cancelled')"
                    );
                });

            modelBuilder.Entity<TransferOrderItem>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<TransferOrderItem>()
                .Property(t => t.Quantity)
                .IsRequired();

            modelBuilder.Entity<TransferOrderItem>()
                .ToTable(t => t.HasCheckConstraint(
                    "CK_TransferOrderItems_Quantity",
                    "[Quantity] > 0"));

            modelBuilder.Entity<TransferOrderItem>()
                .HasOne(t => t.TransferOrder)
                .WithMany(t => t.TransferOrderItems)
                .HasForeignKey(t => t.TransferOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransferOrderItem>()
                .HasOne(t => t.BlanketModel)
                .WithMany(b => b.TransferOrderItems)
                .HasForeignKey(t => t.BlanketModelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockMovement>()
                .HasKey(sm => sm.Id);

            modelBuilder.Entity<StockMovement>()
                .Property(sm => sm.MovementType)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<StockMovement>()
                .Property(sm => sm.Quantity)
                .IsRequired();

            modelBuilder.Entity<StockMovement>()
                .Property(sm => sm.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.BlanketModel)
                .WithMany(b => b.StockMovements)
                .HasForeignKey(sm => sm.BlanketModelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.FactoryInventory)
                .WithMany(fi => fi.StockMovements)
                .HasForeignKey(sm => sm.FactoryInventoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.DistributorInventory)
                .WithMany(di => di.StockMovements)
                .HasForeignKey(sm => sm.DistributorInventoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.SellerInventory)
                .WithMany(si => si.StockMovements)
                .HasForeignKey(sm => sm.SellerInventoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.TransferOrder)
                .WithMany(t => t.StockMovements)
                .HasForeignKey(sm => sm.TransferOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.CustomerOrder)
                .WithMany(c => c.StockMovements)
                .HasForeignKey(sm => sm.CustomerOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockMovement>()
                .ToTable(t =>
                {
                    t.HasCheckConstraint(
                        "CK_StockMovements_Quantity",
                        "[Quantity] > 0");

                    t.HasCheckConstraint(
                        "CK_StockMovements_MovementType",
                        "[MovementType] IN ('Reserve','Release','Issue','Receive','Adjust')");

                    t.HasCheckConstraint(
                        "CK_StockMovements_OnlyOneInventory",
                        @"(
            (CASE WHEN [FactoryInventoryId] IS NOT NULL THEN 1 ELSE 0 END) +
            (CASE WHEN [DistributorInventoryId] IS NOT NULL THEN 1 ELSE 0 END) +
            (CASE WHEN [SellerInventoryId] IS NOT NULL THEN 1 ELSE 0 END)
          ) = 1");
                });

            modelBuilder.Entity<Role>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Role>()
                .Property(r => r.RoleName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.RoleName)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}