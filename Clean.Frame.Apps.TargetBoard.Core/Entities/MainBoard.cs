using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Core.Entities
{
    public  class MainBoard
    {
        
        public int Id { get; set; }

        public int UnitId { get; set; }
        public int Year { get; set; }

        public int Month { get; set; }

        public virtual Unit Unit { get; set; }

        public virtual ICollection<Aspect> Aspects { get; set; }

        
    }
}
