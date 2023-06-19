using Clean.Frame.Apps.TargetBoard.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Models
{
    public class MainBoardJsonViewModel
    {

        
       
        public int Year { get; set; }

        public int Month { get; set; }

        public ICollection<AspectJsonViewModel> Aspects { get; set; }

        public List<string> MonthNames { get; set; }
    }
}
