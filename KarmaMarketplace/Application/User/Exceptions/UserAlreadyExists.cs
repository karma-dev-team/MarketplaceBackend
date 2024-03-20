namespace KarmaMarketplace.Application.User.Exceptions
{
    public class UserAlreadyExists : Exception
    {
        public UserAlreadyExists(string Identifier, string IdentifierKey)
            : base($"User already exists with {IdentifierKey}: {Identifier}") 
        {
        }
    }
}
