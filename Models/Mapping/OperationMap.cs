using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ORM.Models.Mapping
{
    public class OperationMap : EntityTypeConfiguration<Operation>
    {
        public OperationMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Operations");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.idMorgue).HasColumnName("idMorgue");
            this.Property(t => t.idDr).HasColumnName("idDr");
            this.Property(t => t.idPatient).HasColumnName("idPatient");
            this.Property(t => t.date).HasColumnName("date");

            // Relationships
            this.HasRequired(t => t.Morgue)
                .WithMany(t => t.Operations)
                .HasForeignKey(d => d.idMorgue);
            this.HasRequired(t => t.Patient)
                .WithMany(t => t.Operations)
                .HasForeignKey(d => d.idPatient);
            this.HasRequired(t => t.Stuff)
                .WithMany(t => t.Operations)
                .HasForeignKey(d => d.idDr);

        }
    }
}
