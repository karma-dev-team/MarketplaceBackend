using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Domain.Staff.Entities;

namespace KarmaMarketplace.Application.Staff.UseCases
{
    public class DeleteComment : BaseUseCase<Guid, TicketCommentEntity>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAccessPolicy _accessPolicy; 

        public DeleteComment(IApplicationDbContext dbContext, IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy; 
            _context = dbContext;
        }

        public async Task<TicketCommentEntity> Execute(Guid commentId)
        {
            Guard.Against.Null(commentId, nameof(commentId));

            await _accessPolicy.FailIfNoAccess(Domain.User.Enums.UserRoles.Admin);

            var comment = await _context.TicketComments.FindAsync(commentId);

            Guard.Against.Null(comment, $"Comment with ID {commentId} does not exist.");

            _context.TicketComments.Remove(comment);
            await _context.SaveChangesAsync();

            return comment;
        }
    }
}
