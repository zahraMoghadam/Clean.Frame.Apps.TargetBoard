using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Clean.Frame.Apps.TargetBoard.Models
{
    public class TargetViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Score { get; set; }
        public string Progress { get; set; }
        public string? Message { get; set; }
        public List<string> Messages { get; set; }
        public int AspectId { get; set; }

        public AspectViewModel? Aspect { get; set; }

    }
}