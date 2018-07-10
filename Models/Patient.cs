using System;
using System.Collections.Generic;

namespace ORM.Models
{
    public partial class Patient
    {
        public Patient()
        {
            this.BodyParts = new List<BodyPart>();
            this.Operations = new List<Operation>();
        }

        public int id { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public int age { get; set; }
        public System.DateTime deathDate { get; set; }
        public virtual ICollection<BodyPart> BodyParts { get; set; }
        public virtual ICollection<Operation> Operations { get; set; }
    }
}
