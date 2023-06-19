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
    public class MainBoardService : Service<MainBoard>, IMainBoardService
    {
        public IMapper _mapper { get; }

        public MainBoardService(IUnitOfWork unitOfWork, IRepository<MainBoard> repository,IMapper mapper) : base(unitOfWork, repository)
        {
            _mapper = mapper;
        }

        public async Task<MainBoardDto> GetByIdAsync(int Id)
        {
            var data= await UnitOfWork.MainBoardRepository.GetByIdAsync(Id);
            if (data != null)
            {
                return _mapper.Map<MainBoardDto>(data);
            }
            return null;
        }
        public async Task<List<MainBoardDto>> GetAllByDateAsync(int year, int month)
        {
            var data= await UnitOfWork.MainBoardRepository.GetAllRelationAsync(year, month);
            if (data != null)
            {
                return _mapper.Map<List<MainBoardDto>>(data);
            }
            return null;

        }
        public async Task<List<MainBoardDto>> GetAllByUnitAsync()
        {
            var data= await UnitOfWork.MainBoardRepository.GetAllByUnitAsync();
            if (data != null)
            {
                return _mapper.Map<List<MainBoardDto>>(data);
            }
            return null;
        }
        public async Task<bool> CheckHasMainBoardAspectsForDelete(int id)
        {
            var mainboards = await UnitOfWork.MainBoardRepository.GetMainBoardAspectsById(id);

            if (mainboards.Count == 1 && mainboards.Any(x => x.Aspects.Count > 0))
            {
                return true;
            }

            if (mainboards.Any(x=>x.Aspects.Count>0))
            {
                return true;
            }

            return false;
        }
    }
}
