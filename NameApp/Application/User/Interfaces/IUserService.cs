using NameApp.Application.User.Interactors;

namespace NameApp.Application.User.Interfaces
{
    public interface IUserService
    {
        public abstract RegisterInteractor RegisterInteractor(); 
    }
}
