using UnityEngine;

public class ReflexAgent : SimpleAgent
{
    public override Action DetermineAction(bool p)
    {
        int room = x + 1 + 3 * (y + 1);
        if (p) return Action.SUCK;
        return MoveToNextRoom(room);
    }

    public Action MoveToNextRoom(int room)
    {
        switch(room)
        {
            case 0: return Action.RIGHT;
            case 1: return Random.Range(0, 2) == 0 ? Action.UP : Action.RIGHT;
            case 2: return Action.UP;
            case 3: return Random.Range(0,2) == 0 ? Action.DOWN: Action.RIGHT;
            case 4: return (Action)Random.Range(0, 4);
            case 5: return Random.Range(0, 2) == 0 ? Action.UP : Action.LEFT;
            case 6: return Action.DOWN;
            case 7: return Random.Range(0, 2) == 0 ? Action.DOWN : Action.LEFT;
            case 8: return Action.LEFT;
        }
        return Action.DONOTHING;
    }
}
