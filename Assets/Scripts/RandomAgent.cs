using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAgent : SimpleAgent
{
    public override Action DetermineAction(bool p)
    {
        return (Action)Random.Range(0, 6);
    }
}
