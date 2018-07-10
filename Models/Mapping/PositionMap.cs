using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ORM.Models.Mapping
{
    public class PositionMap : EntityTypeConfiguration<Position>
    {
        public PositionMap()
        {
            // Primary Key
            this.HasKey(t => new { t.idMorgue, t.idPerson, t.nameOfPosition, t.salary });

            // Properties
            this.Property(t => t.idMorgue)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.idPerson)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.nameOfPosition)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.salary)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Positions");
            this.Property(t => t.idMorgue).HasColumnName("idMorgue");
            this.Property(t => t.idPerson).HasColumnName("idPerson");
            this.Property(t => t.nameOfPosition).HasColumnName("nameOfPosition");
            this.Property(t => t.salary).HasColumnName("salary");

            // Relationships
            this.HasRequired(t => t.Morgue)
                .WithMany(t => t.Positions)
                .HasForeignKey(d => d.idMorgue);
            this.HasRequired(t => t.Stuff)
                .WithMany(t => t.Positions)
                .HasForeignKey(d => d.idPerson);

        }
    }
}
