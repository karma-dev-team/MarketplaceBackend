namespace KarmaMarketplace.Application.Common.Exceptions
{
    public class EntityDoesNotExists : Exception
    {
        public EntityDoesNotExists(string entityName, string info) 
            : base($"{entityName} does not exists, info: {info}") { }
    }
}
