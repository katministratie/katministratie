using System;

namespace Superkatten.Katministratie.Infrastructure.Mapper;

public static class EnumConverter<T> where T : Enum
{
    public static int ToInt(T value)
    {
        return (int)(ValueType)value;
    }

    public static T FromInt(int value)
    {
        return (T)(ValueType)value;
    }
}
