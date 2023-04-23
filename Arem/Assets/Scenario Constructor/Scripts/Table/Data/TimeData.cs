using System;

[Serializable]
public class TimeData : IGeneralizable
{
    public IGeneralContainer Container { get; set; }

    public ushort DayNumber;
    public EDayPart DayPart;


    public TimeData(ushort dayNumber, EDayPart dayPart)
    {
        DayNumber = dayNumber;
        DayPart = dayPart;
    }


    public override string ToString()
    {
        return $"Сутки {DayNumber} {DayPart.TranslateToRussian()}";
    }
}