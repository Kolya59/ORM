using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ORM.Models.Mapping
{
    public class ListOfPositionMap : EntityTypeConfiguration<ListOfPosition>
    {
        public ListOfPositionMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.officialName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.necessaryExperiance)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ListOfPositions");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.officialName).HasColumnName("officialName");
            this.Property(t => t.necessaryExperiance).HasColumnName("necessaryExperiance");
        }
    }
}
