namespace NameApp.Application.User.Exceptions
{
    public class UserDoesNotExists : Exception
    {
        public UserDoesNotExists(string Identifier, string IdentifierKey)
            : base($"User dies not exists with {IdentifierKey}: {Identifier}")
        {
        }
    }
}
