using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ORM.Models.Mapping
{
    public class BuildingInfoMap : EntityTypeConfiguration<BuildingInfo>
    {
        public BuildingInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.address)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.type)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.typeOfFinancing)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("BuildingInfo");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.address).HasColumnName("address");
            this.Property(t => t.type).HasColumnName("type");
            this.Property(t => t.workersAmount).HasColumnName("workersAmount");
            this.Property(t => t.typeOfFinancing).HasColumnName("typeOfFinancing");
        }
    }
}
