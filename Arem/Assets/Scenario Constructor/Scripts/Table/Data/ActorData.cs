using System;

[Serializable]
public class ActorData : IGeneralizable
{
    public IGeneralContainer Container { get; set; }

    public string Name;


    public ActorData(string name)
    {
        Name = name;
    }


    public override string ToString()
    {
        return $"{Name}";
    }
}