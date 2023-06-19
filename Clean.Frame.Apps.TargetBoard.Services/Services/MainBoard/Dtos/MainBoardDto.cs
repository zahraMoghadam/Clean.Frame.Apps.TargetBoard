using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Services.Services.Dtos
{
    public class MainBoardDto
    {

        public int Id { get; set; }
        public int UnitId { get; set; }
        public int Year { get; set; }

        public int Month { get; set; }

        public string MonthName { get; set; }


        public string UnitName { get; set; }

        //public  UnitViewModel Unit { get; set; }

        public List<SelectListItem> Units { get; set; }


        public List<SelectListItem> Years { get; set; }


        public List<SelectListItem> Months { get; set; }
        public ICollection<AspectDto> Aspects { get; set; }

        public List<string> MonthNames { get; set; }
    }
}
