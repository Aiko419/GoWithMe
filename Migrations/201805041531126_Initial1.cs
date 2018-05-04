namespace GoWithMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "GOWITHME.Customer",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 18, scale: 2, identity: true),
                        AccountID = c.String(maxLength: 250),
                        Name = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(nullable: false, maxLength: 15),
                        Address = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "GOWITHME.Ticket",
                c => new
                    {
                        TourID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Quantyti = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tatus = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.TourID, t.CustomerID })
                .ForeignKey("GOWITHME.Customer", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("GOWITHME.Tour", t => t.TourID, cascadeDelete: true)
                .Index(t => t.TourID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "GOWITHME.Tour",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 18, scale: 2, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Quantyti = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discription = c.String(maxLength: 100),
                        StartDay = c.DateTime(nullable: false),
                        Duration = c.String(maxLength: 50),
                        Image = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "GOWITHME.TourDetail",
                c => new
                    {
                        TourID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PlaceID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Number = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.TourID, t.PlaceID })
                .ForeignKey("GOWITHME.Place", t => t.PlaceID, cascadeDelete: true)
                .ForeignKey("GOWITHME.Tour", t => t.TourID, cascadeDelete: true)
                .Index(t => t.TourID)
                .Index(t => t.PlaceID);
            
            CreateTable(
                "GOWITHME.Place",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 18, scale: 2, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Discription = c.String(maxLength: 200),
                        Image = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "GOWITHME.News",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 18, scale: 2, identity: true),
                        PlaceID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(nullable: false, maxLength: 50),
                        Content = c.String(nullable: false),
                        Image = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("GOWITHME.Place", t => t.PlaceID, cascadeDelete: true)
                .Index(t => t.PlaceID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("GOWITHME.TourDetail", "TourID", "GOWITHME.Tour");
            DropForeignKey("GOWITHME.TourDetail", "PlaceID", "GOWITHME.Place");
            DropForeignKey("GOWITHME.News", "PlaceID", "GOWITHME.Place");
            DropForeignKey("GOWITHME.Ticket", "TourID", "GOWITHME.Tour");
            DropForeignKey("GOWITHME.Ticket", "CustomerID", "GOWITHME.Customer");
            DropIndex("GOWITHME.News", new[] { "PlaceID" });
            DropIndex("GOWITHME.TourDetail", new[] { "PlaceID" });
            DropIndex("GOWITHME.TourDetail", new[] { "TourID" });
            DropIndex("GOWITHME.Ticket", new[] { "CustomerID" });
            DropIndex("GOWITHME.Ticket", new[] { "TourID" });
            DropTable("GOWITHME.News");
            DropTable("GOWITHME.Place");
            DropTable("GOWITHME.TourDetail");
            DropTable("GOWITHME.Tour");
            DropTable("GOWITHME.Ticket");
            DropTable("GOWITHME.Customer");
        }
    }
}
