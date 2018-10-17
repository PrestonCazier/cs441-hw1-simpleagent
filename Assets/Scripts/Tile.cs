using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public bool hasDirt = false;
    public int x, y;

    public void SetLocation(int x, int y)
    {
        this.x = x;
        this.y = y;
        transform.position = new Vector3(x, y, 0);
    }
}
