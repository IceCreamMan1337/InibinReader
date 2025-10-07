namespace LoLINI;

internal static class HashFunctions
{
    public static uint HashString_SDBM(string str)
    {
        uint result = 0;

        if (string.IsNullOrEmpty(str))
        {
            return result;
        }

        foreach (byte character in str.ToLower())
        {
            result = character + 65599 * result;
        }

        return result;
    }

    public static uint HashString_SDBM(string str, string addStr)
    {
        return HashString_SDBM(str + '*' + addStr);
    }
}
