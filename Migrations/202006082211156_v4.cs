namespace CodeFirstUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "CourseTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "CourseTypeID");
            AddForeignKey("dbo.Courses", "CourseTypeID", "dbo.CourseTypes", "CourseTypeID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "CourseTypeID", "dbo.CourseTypes");
            DropIndex("dbo.Courses", new[] { "CourseTypeID" });
            DropColumn("dbo.Courses", "CourseTypeID");
        }
    }
}
