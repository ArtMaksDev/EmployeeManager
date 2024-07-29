namespace Common;

public static class Argument
{
    public static void IsNotNull(object obj, string argName)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(argName);
        }
    }

    public static void IsNotNullOrEmpty(string str, string argName)
    {
        if (string.IsNullOrEmpty(str))
        {
            throw new ArgumentException("Value cannot be null or empty.", argName);
        }
    }
}