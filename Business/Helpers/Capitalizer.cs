namespace Business.Helpers;

public static class Capitalizer
{
    public static string Capitalize(string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;
        
        return char.ToUpper(str[0]) + str.Substring(1);
    }
}