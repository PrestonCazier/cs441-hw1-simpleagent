using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurphyRandomAgent : SimpleAgent
{
    public override Action DetermineAction(bool dirty)
    {
        // 10% chance to flip sensor reading to wrong value
        if (Random.Range(0, 10) == 0)
        {
            dirty = !dirty;
        }
        return (Action) (Random.Range(0,10) == 0 ? Random.Range(0, 6) : Random.Range(5, 10));
    }
}
