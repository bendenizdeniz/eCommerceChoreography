namespace Shared.Extensions;

public static class OrderExtensions
{
    public static Guid StringToGuid(string name, string val)
    {
        return Guid.TryParse(val, out Guid _gVal)
            ? _gVal
            : throw new Exception($"{val} is incompatible.");
    }
}