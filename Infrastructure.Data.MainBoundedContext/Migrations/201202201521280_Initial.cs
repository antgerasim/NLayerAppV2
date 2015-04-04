namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Customers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 100),
                        FullName = c.String(),
                        Telephone = c.String(maxLength: 25),
                        Company = c.String(maxLength: 200),
                        Address_City = c.String(),
                        Address_ZipCode = c.String(),
                        Address_AddressLine1 = c.String(),
                        Address_AddressLine2 = c.String(),
                        CreditLimit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsEnabled = c.Boolean(nullable: false),
                        CountryId = c.Guid(nullable: false),
                        RawPhoto = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "Countries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CountryName = c.String(nullable: false, maxLength: 50),
                        CountryISOCode = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        Title = c.String(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 10, scale: 2),
                        AmountInStock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Orders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        DeliveryDate = c.DateTime(),
                        IsDelivered = c.Boolean(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        SequenceNumberOrder = c.Int(nullable: false, identity: true),
                        ShippingInformation_ShippingName = c.String(),
                        ShippingInformation_ShippingAddress = c.String(),
                        ShippingInformation_ShippingCity = c.String(),
                        ShippingInformation_ShippingZipCode = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "OrderLines",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Amount = c.Int(nullable: false),
                        Discount = c.Decimal(nullable: false, precision: 10, scale: 2),
                        OrderId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Products", t => t.ProductId)
                .ForeignKey("Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "BankAccounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BankAccountNumber_OfficeNumber = c.String(),
                        BankAccountNumber_NationalBankCode = c.String(),
                        BankAccountNumber_AccountNumber = c.String(),
                        BankAccountNumber_CheckDigits = c.String(),
                        Iban = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 14, scale: 2),
                        Locked = c.Boolean(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "BankAccountActivities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BankAccountId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ActivityDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BankAccounts", t => t.BankAccountId, cascadeDelete: true)
                .Index(t => t.BankAccountId);
            
            CreateTable(
                "Softwares",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LicenseCode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Products", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "Books",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Publisher = c.String(nullable: false),
                        ISBN = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Products", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("Books", new[] { "Id" });
            DropIndex("Softwares", new[] { "Id" });
            DropIndex("BankAccountActivities", new[] { "BankAccountId" });
            DropIndex("BankAccounts", new[] { "CustomerId" });
            DropIndex("OrderLines", new[] { "OrderId" });
            DropIndex("OrderLines", new[] { "ProductId" });
            DropIndex("Orders", new[] { "CustomerId" });
            DropIndex("Customers", new[] { "CountryId" });
            DropForeignKey("Books", "Id", "Products");
            DropForeignKey("Softwares", "Id", "Products");
            DropForeignKey("BankAccountActivities", "BankAccountId", "BankAccounts");
            DropForeignKey("BankAccounts", "CustomerId", "Customers");
            DropForeignKey("OrderLines", "OrderId", "Orders");
            DropForeignKey("OrderLines", "ProductId", "Products");
            DropForeignKey("Orders", "CustomerId", "Customers");
            DropForeignKey("Customers", "CountryId", "Countries");
            DropTable("Books");
            DropTable("Softwares");
            DropTable("BankAccountActivities");
            DropTable("BankAccounts");
            DropTable("OrderLines");
            DropTable("Orders");
            DropTable("Products");
            DropTable("Countries");
            DropTable("Customers");
        }
    }
}
