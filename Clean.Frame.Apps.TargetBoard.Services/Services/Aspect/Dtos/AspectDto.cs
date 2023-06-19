
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Services.Services.Dtos
{
    public class AspectDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Weight { get; set; }
        public string Score { get; set; }
        public string Progress { get; set; }
        public int MainBoardId { get; set; }
        public ICollection<TargetDto> Targets { get; set; }

        public MainBoardDto MainBoard { get; set; }

        public AspectDto()
        {
        }
    }
}
