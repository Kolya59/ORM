using System;
using System.Collections.Generic;

namespace ORM.Models
{
    public partial class BuildingInfo
    {
        public BuildingInfo()
        {
            this.Buildings = new List<Building>();
        }

        public int id { get; set; }
        public string address { get; set; }
        public string type { get; set; }
        public int workersAmount { get; set; }
        public string typeOfFinancing { get; set; }
        public virtual ICollection<Building> Buildings { get; set; }
    }
}
