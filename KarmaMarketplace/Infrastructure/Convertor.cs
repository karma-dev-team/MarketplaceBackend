using System.Reflection;

namespace KarmaMarketplace.Infrastructure
{
    public static class Convertor
    {
        public static Dictionary<string, object> ConvertToDictionary(object obj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            PropertyInfo[] properties = obj.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                result.Add(property.Name, property.GetValue(obj));
            }

            return result;
        }
    }
}
