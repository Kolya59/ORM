using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ORM.Models.Mapping
{
    public class BodyPartsInfoMap : EntityTypeConfiguration<BodyPartsInfo>
    {
        public BodyPartsInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.type);

            // Properties
            this.Property(t => t.type)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.shelfLife)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("BodyPartsInfo");
            this.Property(t => t.type).HasColumnName("type");
            this.Property(t => t.shelfLife).HasColumnName("shelfLife");
            this.Property(t => t.fragility).HasColumnName("fragility");
        }
    }
}
