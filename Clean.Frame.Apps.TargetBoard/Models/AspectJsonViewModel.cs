using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Clean.Frame.Apps.TargetBoard.Models
{
    public class AspectJsonViewModel
    {
      
        public string Title { get; set; }
        public string Weight { get; set; }
        public string Score { get; set; }
        public string Progress { get; set; }
      
        public ICollection<TargetJsonViewModel> Targets { get; set; }  

    }
}
