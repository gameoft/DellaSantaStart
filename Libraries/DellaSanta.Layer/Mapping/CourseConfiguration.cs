using System.Data.Entity.ModelConfiguration;
using DellaSantaStart.Core;

namespace DellaSanta.Layer
{
    public class CourseConfiguration : EntityTypeConfiguration<Course>
    {
        public CourseConfiguration()
        {

            Property(c => c.CourseName)
                .HasMaxLength(250)
                .IsFixedLength()
                .IsRequired();

            //HasMany<EnrolledClass>(g => g.EnrolledClasses)
            //.WithRequired(s => s.Course)
            //.HasForeignKey<int>(s => s.CourseId)
            //.WillCascadeOnDelete(false);

            //HasRequired<User>(x => x.Teacher)
            //    .WithMany(x => x.CoursesTaught)
            //    .HasForeignKey<int>(x => x.TeacherId)
            //    .WillCascadeOnDelete(false);
        }
    }
}
