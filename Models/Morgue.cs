using System;
using System.Collections.Generic;

namespace ORM.Models
{
    public partial class Morgue
    {
        public Morgue()
        {
            this.Operations = new List<Operation>();
            this.Positions = new List<Position>();
        }

        public int idMorgue { get; set; }
        public string adress { get; set; }
        public string typeOfMorgue { get; set; }
        public string officialName { get; set; }
        public virtual ICollection<Operation> Operations { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
    }
}
