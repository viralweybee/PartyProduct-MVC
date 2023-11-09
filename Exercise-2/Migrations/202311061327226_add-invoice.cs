namespace Exercise_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addinvoice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        PartyId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        rate = c.Int(nullable: false),
                        time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Parties", t => t.PartyId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.PartyId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Invoices", "PartyId", "dbo.Parties");
            DropIndex("dbo.Invoices", new[] { "ProductId" });
            DropIndex("dbo.Invoices", new[] { "PartyId" });
            DropTable("dbo.Invoices");
        }
    }
}
