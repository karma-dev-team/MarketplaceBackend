using KarmaMarketplace.Application.Staff.Interfaces;
using KarmaMarketplace.Application.Staff.UseCases;

namespace KarmaMarketplace.Application.Staff
{
    public class StaffService : IStaffService
    {
        private readonly IServiceProvider _serviceProvider;

        public StaffService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public CreateComment CreateComment()
        {
            return _serviceProvider.GetRequiredService<CreateComment>();
        }
        public CreateTicket CreateTicket()
        {
            return _serviceProvider.GetRequiredService<CreateTicket>(); 
        }
        public DeleteComment DeleteComment()
        {
            return _serviceProvider.GetRequiredService<DeleteComment>(); 
        }
        public DeleteTicket DeleteTicket()
        {
            return _serviceProvider.GetRequiredService<DeleteTicket>();
        }
        public UpdateTicket UpdateTicket()
        {
            return _serviceProvider.GetRequiredService<UpdateTicket>();
        }
    }
}
