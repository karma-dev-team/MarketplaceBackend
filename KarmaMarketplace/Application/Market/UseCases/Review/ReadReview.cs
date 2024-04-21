using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Infrastructure.Data.Extensions;
using KarmaMarketplace.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Market.UseCases.Review
{
    public class GetReviewsList : BaseUseCase<GetReviewsListDto, ICollection<ReviewEntity>>
    {
        private IApplicationDbContext _context; 

        public GetReviewsList(IApplicationDbContext dbContext) {
            _context = dbContext; 
        }

        public async Task<ICollection<ReviewEntity>> Execute(GetReviewsListDto dto)
        {
            var query = _context.Reviews
                .AsNoTracking()
                .IncludeStandard()
                .AsQueryable(); 

            if (dto.ChatID != null)
            {
                query = query.Where(x => x.Purchase.ChatId == dto.ChatID); 
            } 
            if (dto.UserId != null)
            {
                query = query.Where(x => x.CreatedBy.Id == dto.UserId);
            }

            query = query.Paginate(dto.Start, dto.Ends); 

            var result = await query.ToListAsync(); 

            return result;
        }
    }
}
