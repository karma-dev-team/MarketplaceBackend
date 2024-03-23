namespace KarmaMarketplace.Application.Common.Exceptions
{
    public class EntityAlreadyExists : Exception
    {
        public EntityAlreadyExists(string entityName, object identifier, string info) 
            : base($"{entityName} does exists, with key: {identifier}, info: {info}") { }
    }
}
