using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using DellaSanta.Core;

namespace DellaSanta.Layer
{
    public class CoursePathConfiguration : EntityTypeConfiguration<CoursePath>
    {
        public CoursePathConfiguration()
        {
            Property(c => c.CoursePathName)
                .HasMaxLength(250)
                .IsFixedLength()
                .IsRequired()
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("AK_CoursePath_CoursePathName") { IsUnique = true }));

            HasMany<Course>(g => g.Courses)
                .WithRequired(s => s.CoursePath)
                .HasForeignKey<int>(s => s.CoursePathId)
                .WillCascadeOnDelete(false);



        }
    }
}
