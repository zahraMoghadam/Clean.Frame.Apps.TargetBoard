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
    public class MainBoardViewModel
    {

        
        public int Id { get; set; }

       
        public int UnitId { get; set; }
        public int Year { get; set; }

        public int Month { get; set; }

       
        public string MonthName { get; set; }

       
        public string UnitName { get; set; }

        //public  UnitViewModel Unit { get; set; }

      
        public List<SelectListItem> Units{ get; set; }

       
        public List<SelectListItem> Years{ get; set; }

       
        public List<SelectListItem> Months{ get; set; }
        public ICollection<AspectViewModel> Aspects { get; set; }

        public List<string> MonthNames { get; set; }
    }
}
