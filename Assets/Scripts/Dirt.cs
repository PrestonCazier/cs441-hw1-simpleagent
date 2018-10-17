using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    private int x, y;

    public void SetSpot(int x, int y)
    {
        this.x = x;
        this.y = y;
        transform.position = new Vector3(x, y, 0);
    }
}
