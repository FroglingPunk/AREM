//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public abstract class Action
//{
//    public abstract void Execute();
//}

//public abstract class ActionHeroPosition : Action
//{

//}

//public class ActionMovement : ActionHeroPosition
//{
//    public readonly IPositionOccupiable Movable;
//    public readonly FieldLevelPosition Position;


//    public ActionMovement(IPositionOccupiable movable, FieldLevelPosition position)
//    {
//        Movable = movable;
//        Position = position;
//    }


//    public override void Execute()
//    {
//        if (Movable.Position)
//        {
//            Movable.Position.Occupiable = null;
//        }

//        Position.Occupiable = Movable;
//        Movable.Position = Position;
//        Movable.Transform.position = Position.transform.position;
//    }
//}