using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.Frame.Apps.TargetBoard.Models
{
    public class HomeViewModel
    {
        public int Month { get; set; }
        public int Year { get; set; }
       
        public List<SelectListItem> Years { get; set; }

        public List<SelectListItem> Months { get; set; }
    }
}
