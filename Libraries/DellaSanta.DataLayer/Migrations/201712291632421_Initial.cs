namespace DellaSanta.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UploadedFiles",
                c => new
                    {
                        UploadedFilesId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameOnDisk = c.String(nullable: false, maxLength: 256, fixedLength: true),
                        IsProcessed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UploadedFilesId)
                .Index(t => t.NameOnDisk, unique: true, name: "AK_UploadedFiles_UploadedFilesName");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.UploadedFiles", "AK_UploadedFiles_UploadedFilesName");
            DropTable("dbo.UploadedFiles");
        }
    }
}
