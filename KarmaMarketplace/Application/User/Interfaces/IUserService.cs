using KarmaMarketplace.Application.User.Interactors;
using KarmaMarketplace.Application.User.Interactors;

namespace KarmaMarketplace.Application.User.Interfaces
{
    public interface IUserService
    {
        public abstract CreateUser Register();

        public abstract UpdateUser Update();
        public abstract DeleteUser Delete();
        public abstract GetUser Get();
        public abstract GetUsersList GetList(); 
    }
}
