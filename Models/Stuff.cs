using System;
using System.Collections.Generic;

namespace ORM.Models
{
    public partial class Stuff
    {
        public Stuff()
        {
            this.Operations = new List<Operation>();
            this.Positions = new List<Position>();
        }

        public int id { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public int age { get; set; }
        public System.DateTime employmentDate { get; set; }
        public virtual ICollection<Operation> Operations { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
    }
}
