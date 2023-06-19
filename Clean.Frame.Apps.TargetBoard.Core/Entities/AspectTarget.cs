using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Core.Entities
{
    public class AspectTarget
    {
        public int AspectTargetId { get; set; }

        public int AspectId { get; set; }
        public int TargetId { get; set; }

        public  Aspect Aspect { get; set; }
        public Target Target { get; set; }
    }
}
