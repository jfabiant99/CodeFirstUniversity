namespace CodeFirstUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Activo", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "Activo");
        }
    }
}
