using System.ComponentModel;
using System.Reflection;

namespace HarborDispatch;

public static class StatusText
{
    public static string GetDescription(Enum? value)
    {
        if (value is null)
        {
            return string.Empty;
        }

        var name = value.ToString();
        if (value is not (ShipmentStatus or ServiceLevel))
        {
            return name;
        }

        var field = value.GetType().GetField(name);
        var description = field?.GetCustomAttribute<DescriptionAttribute>();
        return description?.Description ?? name;
    }
}
