namespace LatihanMVVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemPenjualans",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NamaBarang = c.String(maxLength: 50, storeType: "nvarchar"),
                        Jumlah = c.Int(nullable: false),
                        Harga = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiskonPersen = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ItemPenjualans");
        }
    }
}
