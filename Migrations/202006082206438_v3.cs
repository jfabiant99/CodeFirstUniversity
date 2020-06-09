namespace CodeFirstUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseTypes",
                c => new
                    {
                        CourseTypeID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CourseTypeID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CourseTypes");
        }
    }
}
