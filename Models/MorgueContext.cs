using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ORM.Models.Mapping;

namespace ORM.Models
{
    public partial class MorgueContext : DbContext
    {
        static MorgueContext()
        {
            Database.SetInitializer<MorgueContext>(null);
        }

        public MorgueContext()
            : base("MorgueContext")
        {
        }

        public DbSet<BodyPart> BodyParts { get; set; }
        public DbSet<BodyPartsInfo> BodyPartsInfoes { get; set; }
        public DbSet<BuildingInfo> BuildingInfoes { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<ListOfBuilding> ListOfBuildings { get; set; }
        public DbSet<ListOfPosition> ListOfPositions { get; set; }
        public DbSet<Morgue> Morgues { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Stuff> Stuffs { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BodyPartMap());
            modelBuilder.Configurations.Add(new BodyPartsInfoMap());
            modelBuilder.Configurations.Add(new BuildingInfoMap());
            modelBuilder.Configurations.Add(new BuildingMap());
            modelBuilder.Configurations.Add(new ListOfBuildingMap());
            modelBuilder.Configurations.Add(new ListOfPositionMap());
            modelBuilder.Configurations.Add(new MorgueMap());
            modelBuilder.Configurations.Add(new OperationMap());
            modelBuilder.Configurations.Add(new PatientMap());
            modelBuilder.Configurations.Add(new PositionMap());
            modelBuilder.Configurations.Add(new StuffMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
        }
    }
}
