using System;
using System.Collections.Generic;

namespace ORM.Models
{
    public partial class Position
    {
        public int idMorgue { get; set; }
        public int idPerson { get; set; }
        public string nameOfPosition { get; set; }
        public string salary { get; set; }
        public virtual Morgue Morgue { get; set; }
        public virtual Stuff Stuff { get; set; }
    }
}
