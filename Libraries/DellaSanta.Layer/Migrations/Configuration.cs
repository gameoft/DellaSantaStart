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
            #region coursepaths

            context.CoursePaths.AddOrUpdate(
                c => c.CoursePathId,
                new CoursePath
                {
                    CoursePathId = 1,
                    CoursePathName = "Applied Math"
                });


            context.CoursePaths.AddOrUpdate(
                c => c.CoursePathId,
                new CoursePath
                {
                    CoursePathId = 2,
                    CoursePathName = "Logistics"
                });

            context.SaveChanges();

            #endregion coursepaths

            #region course
            
            context.Courses.AddOrUpdate(
                c => new { c.CoursePathId, c.CourseName },
                new Course
                {
                    CourseName = "Mathematics 1",
                    CoursePathId = 1,
                });

           
            context.Courses.AddOrUpdate(
                c => new { c.CoursePathId, c.CourseName },
                new Course
                {
                    CourseName = "Mathematics 2",
                    CoursePathId = 1,
                });

            context.Courses.AddOrUpdate(
            c => new { c.CoursePathId, c.CourseName },
            new Course
            {
                CourseName = "Gentle Introduction to Logistics",
                CoursePathId = 2,
            });



            context.SaveChanges();

            #endregion course
        }
    }
}
