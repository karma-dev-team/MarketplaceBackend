namespace NameApp.Domain.AccessService.ValueObjects
{
    public class PermissionCode : ValueObject
    {
        public string Code { get; set; }

        public PermissionCode(string code) {
            Code = code; 
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new[] { Code };
        }
    }
}
