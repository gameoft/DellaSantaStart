using System.Data.Entity;
using DellaSantaStart.Core;

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

        public DbSet<Course> Courses { get; set; }
        public DbSet<CoursePath> CoursePaths { get; set; }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CourseConfiguration());
            modelBuilder.Configurations.Add(new CoursePathConfiguration());
    
            base.OnModelCreating(modelBuilder);
        }
    }
}
