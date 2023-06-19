using Clean.Frame.Apps.TargetBoard.Core.Entities;
using Clean.Frame.Apps.TargetBoard.Core.Repositories;
using Clean.Frame.Apps.TargetBoard.Core.Services;
using Clean.Frame.Apps.TargetBoard.Core.UnitOfWorks;
using System.Threading.Tasks;
using Clean.Frame.Apps.TargetBoard.Services.Services;
using Clean.Frame.Apps.TargetBoard.Services.Services.Dtos;
using AutoMapper;

namespace Clean.Frame.Apps.TargetBoard.Service.Services
{
    public class AspectService : Service<Aspect>, IAspectService
    {
        private readonly IMapper _mapper;

        public AspectService(IUnitOfWork unitOfWork, IRepository<Aspect> repository,IMapper mapper) : base(unitOfWork, repository)
        {
            _mapper = mapper;
        }

        public async Task<AspectDto> GetByIdAsync(int Id)
        {
            var data= await UnitOfWork.AspectRepository.GetByIdAsync(Id);
            if (data != null)
            {
                return _mapper.Map<AspectDto>(data);
            }
            return null;
        }
        public async Task<List<AspectDto>> GetByMainBoardIdAsync(int mainBoardId)
        {
            var data= await UnitOfWork.AspectRepository.GetByMainBoardIdAsync(mainBoardId);
            if (data != null)
            {
                return _mapper.Map<List<AspectDto>>(data);
            }
            return null;
        } 
        
        public async Task<List<AspectDto>> GetByMainBoardIdWithoutRelationAsync(int mainBoardId)
        {
            var data= await UnitOfWork.AspectRepository.GetByMainBoardIdWithoutRelationAsync(mainBoardId);
            if (data != null)
            {
                return _mapper.Map<List<AspectDto>>(data);
            }
            return null;
        }
        public async Task<bool> CheckHasAspectRelationForDelete(int id)
        {
            var aspects = await UnitOfWork.AspectRepository.GetAspectTargetsById(id);

            if (aspects.Count == 1 && aspects.Any(x => x.Targets.Count > 0))
            {
                return true;
            }

            if (aspects.Any(x=>x.Targets.Count>0))
            {
                return true;
            }
            return false;
        }
    }
}
