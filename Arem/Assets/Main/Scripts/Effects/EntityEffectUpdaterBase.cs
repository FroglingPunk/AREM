using System;

public abstract class EntityEffectUpdaterBase
{
    public abstract event Action Updated;


    public abstract void Init();
    public abstract void Deinit();
}