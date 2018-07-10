using System;
using System.Collections.Generic;

namespace ORM.Models
{
    public partial class ListOfBuilding
    {
        public int idBuilding { get; set; }
        public string address { get; set; }
        public int floorCount { get; set; }
        public bool needForRepair { get; set; }
    }
}
