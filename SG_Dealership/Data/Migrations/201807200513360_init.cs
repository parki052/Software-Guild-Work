namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Street1 = c.String(),
                        Street2 = c.String(),
                        City = c.String(),
                        ZipCode = c.String(),
                        CustState_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.States", t => t.CustState_Id)
                .Index(t => t.CustState_Id);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateAbbreviation = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Conditions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Message = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        CustAddress_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.CustAddress_Id)
                .Index(t => t.CustAddress_Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SaleDate = c.DateTime(),
                        Buyer_Id = c.Int(),
                        Employee_Id = c.String(maxLength: 128),
                        PurchasedVehicle_Id = c.Int(),
                        SaleType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Buyer_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Employee_Id)
                .ForeignKey("dbo.Vehicles", t => t.PurchasedVehicle_Id)
                .ForeignKey("dbo.PurchaseTypes", t => t.SaleType_Id)
                .Index(t => t.Buyer_Id)
                .Index(t => t.Employee_Id)
                .Index(t => t.PurchasedVehicle_Id)
                .Index(t => t.SaleType_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsFeatured = c.Boolean(nullable: false),
                        Year = c.Int(nullable: false),
                        Mileage = c.Int(nullable: false),
                        VIN = c.String(),
                        MSRP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        PicturePath = c.String(),
                        BodyStyle_Id = c.Int(),
                        ConditionType_Id = c.Int(),
                        ExteriorColor_Id = c.Int(),
                        InteriorColor_Id = c.Int(),
                        ModelType_Id = c.Int(),
                        Trans_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Styles", t => t.BodyStyle_Id)
                .ForeignKey("dbo.Conditions", t => t.ConditionType_Id)
                .ForeignKey("dbo.Colors", t => t.ExteriorColor_Id)
                .ForeignKey("dbo.Colors", t => t.InteriorColor_Id)
                .ForeignKey("dbo.Models", t => t.ModelType_Id)
                .ForeignKey("dbo.Transmissions", t => t.Trans_Id)
                .Index(t => t.BodyStyle_Id)
                .Index(t => t.ConditionType_Id)
                .Index(t => t.ExteriorColor_Id)
                .Index(t => t.InteriorColor_Id)
                .Index(t => t.ModelType_Id)
                .Index(t => t.Trans_Id);
            
            CreateTable(
                "dbo.Styles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateAdded = c.DateTime(),
                        Maker_Id = c.Int(),
                        UserAddedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Makes", t => t.Maker_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserAddedBy_Id)
                .Index(t => t.Maker_Id)
                .Index(t => t.UserAddedBy_Id);
            
            CreateTable(
                "dbo.Makes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateAdded = c.DateTime(),
                        UserAddedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserAddedBy_Id)
                .Index(t => t.UserAddedBy_Id);
            
            CreateTable(
                "dbo.Transmissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PurchaseTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Sales", "SaleType_Id", "dbo.PurchaseTypes");
            DropForeignKey("dbo.Sales", "PurchasedVehicle_Id", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "Trans_Id", "dbo.Transmissions");
            DropForeignKey("dbo.Vehicles", "ModelType_Id", "dbo.Models");
            DropForeignKey("dbo.Models", "UserAddedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Makes", "UserAddedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Models", "Maker_Id", "dbo.Makes");
            DropForeignKey("dbo.Vehicles", "InteriorColor_Id", "dbo.Colors");
            DropForeignKey("dbo.Vehicles", "ExteriorColor_Id", "dbo.Colors");
            DropForeignKey("dbo.Vehicles", "ConditionType_Id", "dbo.Conditions");
            DropForeignKey("dbo.Vehicles", "BodyStyle_Id", "dbo.Styles");
            DropForeignKey("dbo.Sales", "Employee_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sales", "Buyer_Id", "dbo.Customers");
            DropForeignKey("dbo.Customers", "CustAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "CustState_Id", "dbo.States");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Makes", new[] { "UserAddedBy_Id" });
            DropIndex("dbo.Models", new[] { "UserAddedBy_Id" });
            DropIndex("dbo.Models", new[] { "Maker_Id" });
            DropIndex("dbo.Vehicles", new[] { "Trans_Id" });
            DropIndex("dbo.Vehicles", new[] { "ModelType_Id" });
            DropIndex("dbo.Vehicles", new[] { "InteriorColor_Id" });
            DropIndex("dbo.Vehicles", new[] { "ExteriorColor_Id" });
            DropIndex("dbo.Vehicles", new[] { "ConditionType_Id" });
            DropIndex("dbo.Vehicles", new[] { "BodyStyle_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Sales", new[] { "SaleType_Id" });
            DropIndex("dbo.Sales", new[] { "PurchasedVehicle_Id" });
            DropIndex("dbo.Sales", new[] { "Employee_Id" });
            DropIndex("dbo.Sales", new[] { "Buyer_Id" });
            DropIndex("dbo.Customers", new[] { "CustAddress_Id" });
            DropIndex("dbo.Addresses", new[] { "CustState_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PurchaseTypes");
            DropTable("dbo.Transmissions");
            DropTable("dbo.Makes");
            DropTable("dbo.Models");
            DropTable("dbo.Styles");
            DropTable("dbo.Vehicles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Sales");
            DropTable("dbo.Customers");
            DropTable("dbo.Contacts");
            DropTable("dbo.Conditions");
            DropTable("dbo.Colors");
            DropTable("dbo.States");
            DropTable("dbo.Addresses");
        }
    }
}
