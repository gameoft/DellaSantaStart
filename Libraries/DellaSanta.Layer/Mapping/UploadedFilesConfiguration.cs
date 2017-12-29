using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using DellaSanta.Core;

namespace DellaSanta.Layer
{
    public class UploadedFilesConfiguration : EntityTypeConfiguration<UploadedeFiles>
    {
        public UploadedFilesConfiguration()
        {
            Property(c => c.NameOnDisk)
                .HasMaxLength(4096)
                .IsFixedLength()
                .IsRequired()
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("AK_UploadedFiles_UploadedFilesName") { IsUnique = true }));
            
        }
    }
}
