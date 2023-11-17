namespace Exercise_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesInvoice : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "PartyId", "dbo.Parties");
            DropForeignKey("dbo.Invoices", "ProductId", "dbo.Products");
            DropIndex("dbo.Invoices", new[] { "PartyId" });
            DropIndex("dbo.Invoices", new[] { "ProductId" });
            RenameColumn(table: "dbo.Invoices", name: "PartyId", newName: "Party_id");
            RenameColumn(table: "dbo.Invoices", name: "ProductId", newName: "Product_id");
            AddColumn("dbo.Invoices", "quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoices", "Party_id", c => c.Int());
            AlterColumn("dbo.Invoices", "Product_id", c => c.Int());
            CreateIndex("dbo.Invoices", "Party_id");
            CreateIndex("dbo.Invoices", "Product_id");
            AddForeignKey("dbo.Invoices", "Party_id", "dbo.Parties", "id");
            AddForeignKey("dbo.Invoices", "Product_id", "dbo.Products", "id");
            DropColumn("dbo.Invoices", "time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "time", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Invoices", "Product_id", "dbo.Products");
            DropForeignKey("dbo.Invoices", "Party_id", "dbo.Parties");
            DropIndex("dbo.Invoices", new[] { "Product_id" });
            DropIndex("dbo.Invoices", new[] { "Party_id" });
            AlterColumn("dbo.Invoices", "Product_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoices", "Party_id", c => c.Int(nullable: false));
            DropColumn("dbo.Invoices", "quantity");
            RenameColumn(table: "dbo.Invoices", name: "Product_id", newName: "ProductId");
            RenameColumn(table: "dbo.Invoices", name: "Party_id", newName: "PartyId");
            CreateIndex("dbo.Invoices", "ProductId");
            CreateIndex("dbo.Invoices", "PartyId");
            AddForeignKey("dbo.Invoices", "ProductId", "dbo.Products", "id", cascadeDelete: true);
            AddForeignKey("dbo.Invoices", "PartyId", "dbo.Parties", "id", cascadeDelete: true);
        }
    }
}
