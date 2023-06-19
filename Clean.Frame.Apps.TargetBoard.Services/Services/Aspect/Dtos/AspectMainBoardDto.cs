
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Services.Services.Dtos
{
    public class AspectMainBoardDto
    {
        public int Id { get; set; }
        public string AspectTitle { get; set; }
        public string AspectWeight { get; set; }
        public string AspectScore { get; set; }
        public string AspectProgress { get; set; }
        public int MainBoardId { get; set; }

        public ICollection<TargetDto> Targets { get; set; }

        public MainBoardDto MainBoard { get; set; }

        public AspectMainBoardDto()
        {
        }
    }
}
