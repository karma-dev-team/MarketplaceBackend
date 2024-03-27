using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Market.Entities;

namespace KarmaMarketplace.Application.Market.UseCases.Options
{
    public class CreateOptions : BaseUseCase<ICollection<CreateOptionDto>, ICollection<OptionEntity>>
    {
        public IApplicationDbContext _context;

        public CreateOptions(IApplicationDbContext dbContext) {
            _context = dbContext;
        }

        public ICollection<OptionEntity> Execute(ICollection<CreateOptionDto> options) {

        }
    }
}
