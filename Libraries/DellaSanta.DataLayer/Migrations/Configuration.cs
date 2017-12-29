namespace DellaSanta.DataLayer.Migrations
{
    using System.Data.Entity.Migrations;
    using DellaSanta.Core;

    internal sealed class Configuration : DbMigrationsConfiguration<DellaSanta.DataLayer.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DellaSanta.DataLayer.ApplicationDbContext context)
        {
            #region uploadedfiles

            context.UploadedFiles.AddOrUpdate(
                c => c.Name,
                new UploadedFiles
                {
                    UploadedFilesId = 1,
                    Name = "gText Test Questions 2017.docx",
                    NameOnDisk = "gText Test Questions 2017.docx",
                    IsProcessed = true
                });

            context.UploadedFiles.AddOrUpdate(
                c => c.Name,
                new UploadedFiles
                {
                    UploadedFilesId = 1,
                    Name = "gText Test Questions 2017.docx",
                    NameOnDisk = "2gText Test Questions 2017.docx",
                    IsProcessed = false
                });


            context.SaveChanges();

            #endregion uploadedfiles

        
        }
    }
}
