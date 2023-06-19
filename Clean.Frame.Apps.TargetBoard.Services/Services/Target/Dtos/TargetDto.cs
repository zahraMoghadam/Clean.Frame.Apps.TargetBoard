using Clean.Frame.Apps.TargetBoard.Services.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Services.Services.Dtos
{
    public class TargetDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Score { get; set; }
        public string Progress { get; set; }
        public string? Message { get; set; }
        public string[] Messages { get; set; }
        public int AspectId { get; set; }

        public AspectDto? Aspect { get; set; }
    }
}
