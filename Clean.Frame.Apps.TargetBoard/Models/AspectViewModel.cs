using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Clean.Frame.Apps.TargetBoard.Models
{
    public class AspectViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Weight { get; set; }
        public string Score { get; set; }
        public string Progress { get; set; }
        public int MainBoardId { get; set; }

        public ICollection<TargetViewModel> Targets { get; set; }  

        public MainBoardViewModel MainBoard { get; set; }
    }
}
