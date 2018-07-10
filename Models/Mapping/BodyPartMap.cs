using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ORM.Models.Mapping
{
    public class BodyPartMap : EntityTypeConfiguration<BodyPart>
    {
        public BodyPartMap()
        {
            // Primary Key
            this.HasKey(t => new { t.type, t.ownerID });

            // Properties
            this.Property(t => t.type)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ownerID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("BodyParts");
            this.Property(t => t.type).HasColumnName("type");
            this.Property(t => t.ownerID).HasColumnName("ownerID");
            this.Property(t => t.date).HasColumnName("date");

            // Relationships
            this.HasRequired(t => t.BodyPartsInfo)
                .WithMany(t => t.BodyParts)
                .HasForeignKey(d => d.type);
            this.HasRequired(t => t.Patient)
                .WithMany(t => t.BodyParts)
                .HasForeignKey(d => d.ownerID);

        }
    }
}
