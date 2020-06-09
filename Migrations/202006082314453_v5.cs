namespace CodeFirstUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "CourseTypeID", "dbo.CourseTypes");
            DropIndex("dbo.Courses", new[] { "CourseTypeID" });
            DropPrimaryKey("dbo.Courses");
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartamentID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Budget = c.Decimal(nullable: false, storeType: "money"),
                        StartDate = c.DateTime(nullable: false),
                        InstructorID = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.DepartamentID)
                .ForeignKey("dbo.People", t => t.InstructorID)
                .Index(t => t.InstructorID);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        HireDate = c.DateTime(),
                        EnrollmentDate = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OfficeAssignments",
                c => new
                    {
                        InstructorID = c.Int(nullable: false),
                        Location = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.InstructorID)
                .ForeignKey("dbo.People", t => t.InstructorID)
                .Index(t => t.InstructorID);
            
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        EnrollmentID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                        Grade = c.Int(),
                    })
                .PrimaryKey(t => t.EnrollmentID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.InstructorCourses",
                c => new
                    {
                        Instructor_ID = c.Int(nullable: false),
                        Course_CourseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Instructor_ID, t.Course_CourseID })
                .ForeignKey("dbo.People", t => t.Instructor_ID, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_CourseID, cascadeDelete: true)
                .Index(t => t.Instructor_ID)
                .Index(t => t.Course_CourseID);
            
            AddColumn("dbo.Courses", "Credits", c => c.Int(nullable: false));
            AddColumn("dbo.Courses", "DepartmentID", c => c.Int(nullable: false));
            AddColumn("dbo.Courses", "Department_DepartamentID", c => c.Int());
            AlterColumn("dbo.Courses", "CourseID", c => c.Int(nullable: false));
            AlterColumn("dbo.Courses", "Title", c => c.String(maxLength: 50));
            AddPrimaryKey("dbo.Courses", "CourseID");
            CreateIndex("dbo.Courses", "Department_DepartamentID");
            AddForeignKey("dbo.Courses", "Department_DepartamentID", "dbo.Departments", "DepartamentID");
            DropColumn("dbo.Courses", "CourseTypeID");
            DropTable("dbo.CourseTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CourseTypes",
                c => new
                    {
                        CourseTypeID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CourseTypeID);
            
            AddColumn("dbo.Courses", "CourseTypeID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Enrollments", "StudentID", "dbo.People");
            DropForeignKey("dbo.Enrollments", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.Courses", "Department_DepartamentID", "dbo.Departments");
            DropForeignKey("dbo.Departments", "InstructorID", "dbo.People");
            DropForeignKey("dbo.OfficeAssignments", "InstructorID", "dbo.People");
            DropForeignKey("dbo.InstructorCourses", "Course_CourseID", "dbo.Courses");
            DropForeignKey("dbo.InstructorCourses", "Instructor_ID", "dbo.People");
            DropIndex("dbo.InstructorCourses", new[] { "Course_CourseID" });
            DropIndex("dbo.InstructorCourses", new[] { "Instructor_ID" });
            DropIndex("dbo.Enrollments", new[] { "StudentID" });
            DropIndex("dbo.Enrollments", new[] { "CourseID" });
            DropIndex("dbo.OfficeAssignments", new[] { "InstructorID" });
            DropIndex("dbo.Departments", new[] { "InstructorID" });
            DropIndex("dbo.Courses", new[] { "Department_DepartamentID" });
            DropPrimaryKey("dbo.Courses");
            AlterColumn("dbo.Courses", "Title", c => c.String());
            AlterColumn("dbo.Courses", "CourseID", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Courses", "Department_DepartamentID");
            DropColumn("dbo.Courses", "DepartmentID");
            DropColumn("dbo.Courses", "Credits");
            DropTable("dbo.InstructorCourses");
            DropTable("dbo.Enrollments");
            DropTable("dbo.OfficeAssignments");
            DropTable("dbo.People");
            DropTable("dbo.Departments");
            AddPrimaryKey("dbo.Courses", "CourseID");
            CreateIndex("dbo.Courses", "CourseTypeID");
            AddForeignKey("dbo.Courses", "CourseTypeID", "dbo.CourseTypes", "CourseTypeID", cascadeDelete: true);
        }
    }
}
