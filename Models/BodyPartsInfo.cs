using System;
using System.Collections.Generic;

namespace ORM.Models
{
    public partial class BodyPartsInfo
    {
        public BodyPartsInfo()
        {
            this.BodyParts = new List<BodyPart>();
        }

        public string type { get; set; }
        public string shelfLife { get; set; }
        public bool fragility { get; set; }
        public virtual ICollection<BodyPart> BodyParts { get; set; }
    }
}
