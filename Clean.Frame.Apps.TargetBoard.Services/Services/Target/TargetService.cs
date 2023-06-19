using Clean.Frame.Apps.TargetBoard.Core.Entities;
using Clean.Frame.Apps.TargetBoard.Core.Repositories;
using Clean.Frame.Apps.TargetBoard.Core.Services;
using Clean.Frame.Apps.TargetBoard.Core.UnitOfWorks;
using Clean.Frame.Apps.TargetBoard.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Frame.Apps.TargetBoard.Services.Services;
using Clean.Frame.Apps.TargetBoard.Services.Services.Dtos;
using AutoMapper;

namespace Clean.Frame.Apps.TargetBoard.Service.Services
{
    public class TargetService : Service<Target>, ITargetService
    {
        public IMapper _mapper { get; }

        public TargetService(IUnitOfWork unitOfWork, IRepository<Target> repository,IMapper mapper) : base(unitOfWork, repository)
        {
            _mapper = mapper;
        }

        public async Task<TargetDto> GetByIdAsync(int Id)
        {
            var data= await UnitOfWork.TargetRepository.GetByIdAsync(Id);
            if (data != null)
            {
                return _mapper.Map<TargetDto>(data);
            }
            return null;
        }
        public async Task<List<TargetDto>> GetAllByAspectIdAsync(int aspectId)
        {
            var result = await UnitOfWork.TargetRepository.Where(x => x.AspectId == aspectId);
            var data = result.ToList();

            if (data != null)
            {
                return _mapper.Map<List<TargetDto>>(data);
            }
            return null;
        }
        public async Task<bool> CheckHasTargetRelationForDelete(int id)
        {
            var target = await UnitOfWork.TargetRepository.GetAspectTargetsById(id);
            if (target is null)
            {
                return true;
            }

            if (target.Aspect is not  null)
            {
                return true;
            }

            return false;
        }
    }
}

