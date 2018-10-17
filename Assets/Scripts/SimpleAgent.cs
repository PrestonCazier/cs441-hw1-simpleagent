using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimpleAgent : MonoBehaviour {
    public int x, y;
    public SimulationManager sm;
    public enum Action
    {
        UP = 0,
        DOWN = 1,
        LEFT = 2,
        RIGHT = 3,
        SUCK = 4,
        DONOTHING = 5,
        DIRTY_UP = 6,
        DIRTY_DOWN = 7,
        DIRTY_LEFT = 8,
        DIRTY_RIGHT = 9
    }

	public void SetStart (int x, int y)
    {
        transform.position = new Vector3(x, y, 0);
        this.x = x;
        this.y = y;
	}

    public abstract Action DetermineAction(bool p);
	
	// Update is called once per frame
	public void Act (Action a)
    {
		switch(a)
        {
            case Action.UP: MoveUp(false); break;
            case Action.DOWN: MoveDown(false); break;
            case Action.LEFT: MoveLeft(false); break;
            case Action.RIGHT: MoveRight(false); break;
            case Action.SUCK: Suck(); break;
            case Action.DONOTHING: return;
            case Action.DIRTY_UP: MoveUp(true); break;
            case Action.DIRTY_DOWN: MoveDown(true); break;
            case Action.DIRTY_LEFT: MoveLeft(true); break;
            case Action.DIRTY_RIGHT: MoveRight(true); break;
            default: throw new System.Exception("No Action Found");
        }
	}

    private void MoveUp(bool leaveDirty)
    {
        if (leaveDirty)
        {
            sm.DirtyRoom(x, y);
        }
        if (y < 1)
        {
            y++;
            transform.position = new Vector3(x, y, 0);
        }
    }

    private void MoveDown(bool leaveDirty)
    {
        if (leaveDirty)
        {
            sm.DirtyRoom(x, y);
        }
        if (y > -1)
        {
            y--;
            transform.position = new Vector3(x, y, 0);
        }
    }

    private void MoveLeft(bool leaveDirty)
    {
        if (leaveDirty)
        {
            sm.DirtyRoom(x, y);
        }
        if (x > -1)
        {
            x--;
            transform.position = new Vector3(x, y, 0);
        }
    }

    private void MoveRight(bool leaveDirty)
    {
        if (leaveDirty)
        {
            sm.DirtyRoom(x, y);
        }
        if (x < 1)
        {
            x++;
            transform.position = new Vector3(x, y, 0);
        }
    }

    private void Suck()
    {
        sm.Clean(x, y);
    }
}
