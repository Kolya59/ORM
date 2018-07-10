using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ORM.Models.Mapping
{
    public class ListOfBuildingMap : EntityTypeConfiguration<ListOfBuilding>
    {
        public ListOfBuildingMap()
        {
            // Primary Key
            this.HasKey(t => t.idBuilding);

            // Properties
            this.Property(t => t.address)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ListOfBuildings");
            this.Property(t => t.idBuilding).HasColumnName("idBuilding");
            this.Property(t => t.address).HasColumnName("address");
            this.Property(t => t.floorCount).HasColumnName("floorCount");
            this.Property(t => t.needForRepair).HasColumnName("needForRepair");
        }
    }
}
