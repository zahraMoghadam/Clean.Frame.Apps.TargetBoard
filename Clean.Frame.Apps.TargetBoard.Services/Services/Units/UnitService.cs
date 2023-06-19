using AutoMapper;
using Clean.Frame.Apps.TargetBoard.Core.Entities;
using Clean.Frame.Apps.TargetBoard.Core.Repositories;
using Clean.Frame.Apps.TargetBoard.Core.UnitOfWorks;
using Clean.Frame.Apps.TargetBoard.Service.Services;
using Clean.Frame.Apps.TargetBoard.Services.Services.Dtos;

namespace Clean.Frame.Apps.TargetBoard.Services.Services.Units
{
    public class UnitService : Service<Unit>, IUnitService
    {
        private readonly IMapper _mapper;

        public UnitService(IUnitOfWork unitOfWork, IRepository<Unit> repository, IMapper mapper) : base(unitOfWork, repository)
        {
            _mapper = mapper;
        }

        public async Task<UnitDto?> GetByIdAsync(int id)
        {
            var unit = await UnitOfWork.UnitRepository.GetByIdAsync(id);
            if (unit != null)
            {
                return _mapper.Map<UnitDto>(unit);
            }

            return null;
        }

        public async Task<bool> CheckHasUnitRelationForDelete(int id)
        {
            var unit = await UnitOfWork.UnitRepository.GetUnitMainBoardsById(id);
            if (unit is null)
            {
                return true;
            }

            if (unit.MainBoards is not  null)
            {
                return true;
            }

            return false;
        }
    }
}
