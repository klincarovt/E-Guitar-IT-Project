namespace E_Guitar_IT_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDatabase1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CartId = c.String(),
                        GuitarId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        ShoppingCart_UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Guitars", t => t.GuitarId, cascadeDelete: true)
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCart_UserId)
                .Index(t => t.GuitarId)
                .Index(t => t.ShoppingCart_UserId);
            
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "ShoppingCart_UserId", "dbo.ShoppingCarts");
            DropForeignKey("dbo.CartItems", "GuitarId", "dbo.Guitars");
            DropIndex("dbo.CartItems", new[] { "ShoppingCart_UserId" });
            DropIndex("dbo.CartItems", new[] { "GuitarId" });
            DropTable("dbo.ShoppingCarts");
            DropTable("dbo.CartItems");
        }
    }
}
