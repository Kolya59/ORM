using System;
using System.Collections.Generic;

namespace ORM.Models
{
    public partial class BodyPart : IDisposable
    {
        public string type { get; set; }
        public int ownerID { get; set; }
        public System.DateTime date { get; set; }
        public virtual BodyPartsInfo BodyPartsInfo { get; set; }
        public virtual Patient Patient { get; set; }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
