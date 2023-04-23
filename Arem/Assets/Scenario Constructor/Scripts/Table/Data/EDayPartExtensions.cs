using System.Collections.Generic;

public static class EDayPartExtensions
{
    private static Dictionary<EDayPart, string> _translate = new Dictionary<EDayPart, string>
    {
        { EDayPart.Day, "День" },
        { EDayPart.Evening, "Вечер" },
        { EDayPart.Night, "Ночь" }
    };

    public static string TranslateToRussian(this EDayPart dayPart)
    {
        return _translate[dayPart];
    }
}