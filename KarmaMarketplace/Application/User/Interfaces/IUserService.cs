using KarmaMarketplace.Application.User.Interactors;
using KarmaMarketplace.Application.User.UseCases;
using Microsoft.AspNetCore.Identity.Data;

namespace KarmaMarketplace.Application.User.Interfaces
{
    public interface IUserService
    {
        public abstract CreateUser Create();

        public abstract UpdateUser Update();
        public abstract DeleteUser Delete();
        public abstract GetUser Get();
        public abstract GetUsersList GetList();
        public abstract SendResetCode SendResetPasswordCode();
        public abstract ResetPassword ResetPassword();
        public abstract GetNotifications GetNotifications();
        public abstract CreateNotification CreateNotification(); 
    }
}
