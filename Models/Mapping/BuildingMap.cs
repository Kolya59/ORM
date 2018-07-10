using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ORM.Models.Mapping
{
    public class BuildingMap : EntityTypeConfiguration<Building>
    {
        public BuildingMap()
        {
            // Primary Key
            this.HasKey(t => t.idMorgue);

            // Properties
            this.Property(t => t.idMorgue)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Buildings");
            this.Property(t => t.idMorgue).HasColumnName("idMorgue");
            this.Property(t => t.idBuilding).HasColumnName("idBuilding");

            // Relationships
            this.HasRequired(t => t.BuildingInfo)
                .WithMany(t => t.Buildings)
                .HasForeignKey(d => d.idBuilding);

        }
    }
}
