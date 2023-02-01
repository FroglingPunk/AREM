using System;

[Serializable]
public class EntityStats : ICloneable
{
    public int Damage;
    public int Armor;
    public int CriticalDamageChance;
    public int DodgeChance;
    public int Speed;
    public int ActionPoints;


    public object Clone()
    {
        return new EntityStats
        {
            Damage = Damage,
            Armor = Armor,
            CriticalDamageChance = CriticalDamageChance,
            DodgeChance = DodgeChance,
            Speed = Speed,
            ActionPoints = ActionPoints
        };
    }
}