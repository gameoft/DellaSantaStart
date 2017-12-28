namespace DellaSanta.Layer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CoursePaths",
                c => new
                    {
                        CoursePathId = c.Int(nullable: false, identity: true),
                        CoursePathName = c.String(nullable: false, maxLength: 250, fixedLength: true),
                        Department = c.String(),
                    })
                .PrimaryKey(t => t.CoursePathId)
                .Index(t => t.CoursePathName, unique: true, name: "AK_CoursePath_CoursePathName");
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseName = c.String(nullable: false, maxLength: 250, fixedLength: true),
                        CoursePathId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.CoursePaths", t => t.CoursePathId)
                .Index(t => t.CoursePathId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "CoursePathId", "dbo.CoursePaths");
            DropIndex("dbo.Courses", new[] { "CoursePathId" });
            DropIndex("dbo.CoursePaths", "AK_CoursePath_CoursePathName");
            DropTable("dbo.Courses");
            DropTable("dbo.CoursePaths");
        }
    }
}
