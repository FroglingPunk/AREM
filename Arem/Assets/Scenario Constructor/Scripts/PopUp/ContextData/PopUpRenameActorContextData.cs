using System;

[Serializable]
public class PopUpRenameActorContextData : PopUpContextDataBase
{
    public readonly ActorData ActorData;


    public PopUpRenameActorContextData(ActorData actorData)
    {
        ActorData = actorData;
    }
}