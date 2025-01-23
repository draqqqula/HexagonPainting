using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Drawing.Colors;

public enum BitColor
{
    True,
    False,
    Unspecified
}

public static class BitColorExtensions
{
    public static bool IsTrue(this BitColor bitColor)
    {
        return bitColor == BitColor.True;
    }

    public static bool IsFalse(this BitColor bitColor)
    {
        return bitColor == BitColor.False;
    }

    public static bool IsUnspecified(this BitColor bitColor)
    {
        return bitColor == BitColor.Unspecified;
    }
}

public static class BitColorHelper
{
    public static BitColor FromBoolean(bool value)
    {
        if (value)
        {
            return BitColor.True;
        }
        return BitColor.False;
    }

    public static BitColor FromNullableBoolean(bool? nullable)
    {
        switch (nullable)
        {
            case null: return BitColor.Unspecified;
            case true: return BitColor.True;
            case false: return BitColor.False;
        }
    }
}