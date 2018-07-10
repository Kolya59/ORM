using System;
using System.Collections.Generic;

namespace ORM.Models
{
    public partial class Operation
    {
        public int id { get; set; }
        public int idMorgue { get; set; }
        public int idDr { get; set; }
        public int idPatient { get; set; }
        public System.DateTime date { get; set; }
        public virtual Morgue Morgue { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Stuff Stuff { get; set; }
    }
}
