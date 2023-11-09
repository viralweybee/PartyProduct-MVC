namespace Exercise_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class assign : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assigns",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        PartyId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Parties", t => t.PartyId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.PartyId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assigns", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Assigns", "PartyId", "dbo.Parties");
            DropIndex("dbo.Assigns", new[] { "ProductId" });
            DropIndex("dbo.Assigns", new[] { "PartyId" });
            DropTable("dbo.Assigns");
        }
    }
}
