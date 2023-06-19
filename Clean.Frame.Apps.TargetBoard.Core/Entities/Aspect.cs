using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Clean.Frame.Apps.TargetBoard.Core.Entities
{
    public class Aspect
    {
       
        public int Id { get; set; }
        public string Title { get; set; }
        public string Weight { get; set; }
        public string Score{ get; set; }
        public string Progress { get; set; }

       
        public int MainBoardId { get; set; }

        public virtual ICollection<Target> Targets { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]      
        public virtual MainBoard MainBoard { get; set; }
    }
}
