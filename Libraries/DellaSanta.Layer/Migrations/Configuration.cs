namespace DellaSanta.Layer.Migrations
{
    using System.Data.Entity.Migrations;
    using DellaSanta.Core;

    internal sealed class Configuration : DbMigrationsConfiguration<DellaSanta.Layer.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DellaSanta.Layer.ApplicationDbContext context)
        {
            #region uploadedfiles

            context.UploadedFiles.AddOrUpdate(
                c => c.UploadedFilesId,
                new UploadedFiles
                {
                    UploadedFilesId = 1,
                    Name = "gText Test Questions 2017.docx",
                    NameOnDisk = "gText Test Questions 2017.docx"
                });

            
            context.SaveChanges();

            #endregion uploadedfiles

        
        }
    }
}
