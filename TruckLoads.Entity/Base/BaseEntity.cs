using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckLoads.Entity.Base
{
    public abstract class BaseEntity : AuditEntity
    {
        public long Id { get; set; }
    }
}
