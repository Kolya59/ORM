using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ORM.Models.Mapping
{
    public class StuffMap : EntityTypeConfiguration<Stuff>
    {
        public StuffMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.firstName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.secondName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Stuff");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.firstName).HasColumnName("firstName");
            this.Property(t => t.secondName).HasColumnName("secondName");
            this.Property(t => t.age).HasColumnName("age");
            this.Property(t => t.employmentDate).HasColumnName("employmentDate");
        }
    }
}
