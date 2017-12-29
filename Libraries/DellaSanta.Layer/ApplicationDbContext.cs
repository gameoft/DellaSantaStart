using System.Data.Entity;
using DellaSanta.Core;

namespace DellaSanta.Layer
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            //To turn off lazy loading for a particular property, do not make it virtual. To turn off lazy loading for all entities in the context, set its configuration property to false
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public ApplicationDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public DbSet<UploadedFiles> UploadedFiles { get; set; }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UploadedFilesConfiguration());
       
            base.OnModelCreating(modelBuilder);
        }
    }
}
