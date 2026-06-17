using System.Security.Cryptography;

namespace IoTProject.App.Dependencies
{
    public class ApiKeyGenerator
    {

    public static string GenerateApiKey()
    {
        return Convert.ToHexString(
            RandomNumberGenerator.GetBytes(32));
    }
}
}
