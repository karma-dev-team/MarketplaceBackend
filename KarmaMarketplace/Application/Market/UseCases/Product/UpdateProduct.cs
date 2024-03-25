using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Market.Enums;
using KarmaMarketplace.Domain.Market.Exceptions;
using KarmaMarketplace.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Text.Json;

namespace KarmaMarketplace.Application.Market.UseCases.Product
{
    public class UpdateProduct : BaseUseCase<UpdateProductDto, ProductEntity?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAccessPolicy _accessPolicy;

        public UpdateProduct(IApplicationDbContext dbContext, IAccessPolicy accessPolicy)
        {
            _context = dbContext;
            _accessPolicy = accessPolicy;
        }

        public async Task<ProductEntity> Execute(UpdateProductDto dto)
        {
            var product = await _context.Products
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Id == dto.ProductId);

            Guard.Against.Null(product, message: "Product does not exists");

            await _accessPolicy.FailIfNotSelfOrNoAccess(
                dto.ByUserId, 
                product.CreatedBy.Id, 
                Domain.User.Enums.UserRoles.Moderator);

            var user = await _context.Users.FirstOrDefaultAsync(
                x => x.Id == dto.ByUserId);

            Guard.Against.Null(user, message: "No user"); 

            if (dto.ProductStatus != null)
            {
                if (user.Role <= Domain.User.Enums.UserRoles.Moderator)
                {
                    throw new AccessDenied("Access denied"); 
                }

                if (Enum.TryParse(dto.ProductStatus, out ProductStatus status)) {
                    product.Status = status;
                } else
                {
                    throw new IncorrectAttributes("Status", dto.ProductStatus);
                }
            }
            if (dto.Attributes != null)
            {
                // does not matter if error
                ProductEntity.VerifyAttributes(dto.Attributes, product.Category); 
                product.Attributes = JsonSerializer.Serialize(dto.Attributes);
            }
            if (dto.Name != null) {
                product.Name = dto.Name; 
            }
            if (dto.Images != null)
            {
                
            }

            return;
        }
    }
}
