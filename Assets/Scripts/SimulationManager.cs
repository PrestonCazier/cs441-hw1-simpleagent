using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour {
    const int trialsPerAgent = 256;
    const int maxNumTurns = 100;
    const int numAgents = 4;

    public List<int> trialData;
    public List<float> averages;
    public bool running = true;
    public SimpleAgent[] agent;
    public int currentAgent = 0;
    public Tile[] tiles;
    public Dirt[] dirts;
    public Dirt dirtPrefab;
    public int numDirt;
    public int dirtRemaining;
    public int trialCount = 0;
    public float time = 0.0f;
    public float turnLength = .1f;
    public int turnCount = 0;
    public Text count;
    public Image coverImage;
    public Text textTable;

    // Use this for initialization
    void Start ()
    {
        agent[0].sm = this;
        agent[1].sm = this;
        agent[2].sm = this;
        agent[3].sm = this;
        SetupTiles();
        RandomizeDirt();
        RandomizeStartLoc();
	}
    
    public void SetupTiles()
    {
        for(int i = 0; i < 9; i++)
        {
            tiles[i].SetLocation(i % 3 - 1, i / 3 - 1);
        }
    }

    private void RandomizeStartLoc()
    {
        int startLoc = Random.Range(0, 8);
        agent[currentAgent].SetStart(startLoc % 3 - 1, startLoc / 3 - 1);
    }

    public void RandomizeDirt()
    {
        int locToAdd;
        List<int> list = new List<int>();
        dirtRemaining = numDirt;

        locToAdd = Random.Range(0, 9);
        list.Add(locToAdd);
        for (int i = 1; i < numDirt; i++)
        {
            while(true)
            {
                locToAdd = Random.Range(0, 9);
                if (!list.Contains(locToAdd))
                {
                    break;
                }
            }
            list.Add(locToAdd);
        }

        foreach(int spot in list)
        {
            dirts[spot] = Instantiate<Dirt>(dirtPrefab);
            dirts[spot].SetSpot(spot % 3 - 1, spot / 3 - 1);
            tiles[spot].hasDirt = true;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (running)
        {
            int spot = agent[currentAgent].x + (1 + agent[currentAgent].y) * 3 + 1;
            time += Time.deltaTime;
            if (time >= turnLength && turnCount < maxNumTurns)
            {
                agent[currentAgent].Act(agent[currentAgent].DetermineAction(tiles[spot].hasDirt));
                turnCount++;
                count.text = turnCount.ToString();
                time -= turnLength;
            }
            if (dirtRemaining == 0 || turnCount >= maxNumTurns)
            {
                ResetBoard();
            }
        }
	}

    public void Clean(int x, int y)
    {
        int spot = x + 1 + 3 * (y + 1);
        tiles[spot].hasDirt = false;
        if (dirts[spot] != null)
        {
            dirtRemaining--;
            Dirt dirtToRemove = dirts[spot];
            Destroy(dirtToRemove.gameObject);
            dirts[spot] = null;
        }
    }

    public void DirtyRoom(int x, int y)
    {
        int spot = x + 1 + 3 * (y + 1);
        tiles[spot].hasDirt = true;
        if (dirts[spot] == null)
        {
            dirtRemaining++;
            dirts[spot] = Instantiate(dirtPrefab);
            dirts[spot].SetSpot(x, y);
        }
    }

    public void ResetBoard()
    {
        trialCount++;
        if (trialCount >= trialsPerAgent)
        {
            CalcAverage();
            trialCount = 0;
            numDirt += 2;
            if (numDirt >= 7)
            {
                numDirt = 1;
                agent[currentAgent].SetStart(100, 100);
                currentAgent++;
                if(currentAgent >= numAgents)
                {
                    EndTrials();
                    return;
                }
            }
        }
        else
        {
            trialData.Add(turnCount);
        }
        RemoveOldDirt();
        RandomizeStartLoc();
        RandomizeDirt();
        turnCount = 0;
        time = 0.0f;
        count.text = "0";
    }

    public void RemoveOldDirt()
    {
        for(int spot = 0; spot < 9; spot++)
        {
            if (dirts[spot] != null)
            {
                Dirt dirtToRemove = dirts[spot];
                Destroy(dirtToRemove.gameObject);
                dirts[spot] = null;
            }
        }
    }

    public void CalcAverage()
    {
        int total = 0;
        float avg;
        foreach( int v in trialData)
        {
            total += v;
        }
        avg = total / trialData.Count;
        averages.Add(avg);

        trialData.Clear();
    }

    private void EndTrials()
    {
        running = false;
        coverImage.gameObject.SetActive(true);
    }
}
