namespace Exercise_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product_rate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product_Rate",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Pr_id = c.Int(nullable: false),
                        rate = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Products", t => t.Pr_id, cascadeDelete: true)
                .Index(t => t.Pr_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product_Rate", "Pr_id", "dbo.Products");
            DropIndex("dbo.Product_Rate", new[] { "Pr_id" });
            DropTable("dbo.Product_Rate");
        }
    }
}
