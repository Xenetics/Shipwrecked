using UnityEngine;
using System.Collections;

public class BoatManager : MonoBehaviour
{
    private static BoatManager instance = null;
    public static BoatManager Instance { get { return instance; } }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        leftSpawning = false;
        rightSpawning = false;
    }

    [SerializeField]
    private GameObject LeftSpawn;
    [SerializeField]
    private GameObject RightSpawn;

    // prephabs 
    [SerializeField]
    private GameObject smallBoat;
    [SerializeField]
    private GameObject mediumBoat;
    [SerializeField]
    private GameObject largeBoat;

    private float seaLevel = 8.0f;

    private int levelNumber; // what level we are on

    public int boatsAfloat { get; set; } // boats alive currently

    private char[] boatOrder; // char tags for boats in what order

    private int boatsInLevel; // how many boats are in this level
    private int boatsAtStart = 3; // how many ship at the beginning of the level

    private int boatToSpawn = 0; // what boat we are currently on

    public bool leftSpawning { get; set; } // left is currently spawning a boat
    public bool rightSpawning { get; set; } // right is currently spawning a boat
	
	void Start () 
    {
        levelNumber = LevelManager.Instance.levelChosen;
        GetLevelData();
        StarterShips();
	}
	
	void Update () 
    {
        if (GameManager.WhatState() == "playing")
        {
            if (boatsAfloat < 5)
            {
                SpawnNext();
            }
        }
	}

    private void SpawnNext() // spawns the next ship in order for current level 
    {
        if (boatToSpawn < boatsInLevel)
        {
            switch (boatOrder[boatToSpawn])
            {
                case 's':
                    if (!leftSpawning)
                    {
                        Instantiate(smallBoat, new Vector3(LeftSpawn.transform.position.x, seaLevel + 0.1f, -0.3f), Quaternion.identity);
                        boatsAfloat++;
                        boatToSpawn++;
                    }
                    else if (!rightSpawning)
                    {
                        Instantiate(smallBoat, new Vector3(RightSpawn.transform.position.x, seaLevel + 0.1f, -0.3f), Quaternion.identity);
                        boatsAfloat++;
                        boatToSpawn++;
                    }
                    break;
                case 'm':
                    if (!leftSpawning)
                    {
                        Instantiate(mediumBoat, new Vector3(LeftSpawn.transform.position.x, seaLevel + 0.1f, -0.3f), Quaternion.identity);
                        boatsAfloat++;
                        boatToSpawn++;
                    }
                    else if (!rightSpawning)
                    {
                        Instantiate(mediumBoat, new Vector3(RightSpawn.transform.position.x, seaLevel + 0.1f, -0.3f), Quaternion.identity);
                        boatsAfloat++;
                        boatToSpawn++;
                    }
                    break;
                case 'l':
                    if (!leftSpawning)
                    {
                        Instantiate(largeBoat, new Vector3(LeftSpawn.transform.position.x, seaLevel + 0.1f, -0.3f), Quaternion.identity);
                        boatsAfloat++;
                        boatToSpawn++;
                    }
                    else if (!rightSpawning)
                    {
                        Instantiate(largeBoat, new Vector3(RightSpawn.transform.position.x, seaLevel + 0.1f, -0.3f), Quaternion.identity);
                        boatsAfloat++;
                        boatToSpawn++;
                    }
                    break;
            }
        }
    }

    private void StarterShips() // Spawns the first ships for the level
    {
        for(int i = 1; i <= boatsAtStart ; i++)
        {
            switch (boatOrder[boatToSpawn])
            {
                case 's':
                    Instantiate(smallBoat, new Vector3( -10 + (5 * i), seaLevel + 0.1f, -0.3f), Quaternion.identity);
                    boatsAfloat++;
                    boatToSpawn++;
                    break;
                case 'm':
                    Instantiate(mediumBoat, new Vector3(-10 + (5 * i), seaLevel + 0.1f, -0.3f), Quaternion.identity);
                    boatsAfloat++;
                    boatToSpawn++;
                    break;
                case 'l':
                    Instantiate(largeBoat, new Vector3(-10 + (5 * i), seaLevel + 0.1f, -0.3f), Quaternion.identity);
                    boatsAfloat++;
                    boatToSpawn++;
                    break;
            }
        }
    }

    private void GetLevelData() // get how many ships, and what types of ships, and in what order for this level
    {
        LevelData temp = LevelManager.Instance.GetLevel();
        boatsInLevel = temp.totalShips;
        boatOrder = temp.shipOrder;
    }

    private void testLevel() // just for testing till I have level XML File
    {
        boatsInLevel = 10;
        boatOrder = new char[boatsInLevel];
        boatOrder[0] = 's';
        boatOrder[1] = 's';
        boatOrder[2] = 's';
        boatOrder[3] = 's';
        boatOrder[4] = 's';
        boatOrder[5] = 's';
        boatOrder[6] = 's';
        boatOrder[7] = 's';
        boatOrder[8] = 's';
        boatOrder[9] = 's';
    }
}
