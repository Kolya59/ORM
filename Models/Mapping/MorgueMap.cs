using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ORM.Models.Mapping
{
    public class MorgueMap : EntityTypeConfiguration<Morgue>
    {
        public MorgueMap()
        {
            // Primary Key
            this.HasKey(t => t.idMorgue);

            // Properties
            this.Property(t => t.adress)
                .IsRequired();

            this.Property(t => t.typeOfMorgue)
                .IsRequired();

            this.Property(t => t.officialName)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Morgues");
            this.Property(t => t.idMorgue).HasColumnName("idMorgue");
            this.Property(t => t.adress).HasColumnName("adress");
            this.Property(t => t.typeOfMorgue).HasColumnName("typeOfMorgue");
            this.Property(t => t.officialName).HasColumnName("officialName");
        }
    }
}
