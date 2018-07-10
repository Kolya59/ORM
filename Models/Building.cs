using System;
using System.Collections.Generic;

namespace ORM.Models
{
    public partial class Building
    {
        public int idMorgue { get; set; }
        public int idBuilding { get; set; }
        public virtual BuildingInfo BuildingInfo { get; set; }
    }
}
