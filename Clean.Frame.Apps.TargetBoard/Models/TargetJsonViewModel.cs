using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Clean.Frame.Apps.TargetBoard.Models
{
    public class TargetJsonViewModel
    {
       
        public string Title { get; set; }
        public string Score { get; set; }
        public string Progress { get; set; }
       
        public string? Message { get; set; }
        public List<string>? Messages { get; set; }
       

    }
}