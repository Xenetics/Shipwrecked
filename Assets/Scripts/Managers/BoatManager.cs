using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

        levelNumber = LevelManager.Instance.levelChosen;
        GetLevelData();
        boatsLeft = boatsInLevel;
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
    public int boatsLeft; // how many boats remain to be eaten
    private int boatsAtStart = 3; // how many ship at the beginning of the level

    private int boatToSpawn = 0; // what boat we are currently on

    public bool leftSpawning { get; set; } // left is currently spawning a boat
    public bool rightSpawning { get; set; } // right is currently spawning a boat

    private List<GameObject> boats;
	
	void Start () 
    {
        boats = new List<GameObject>();    
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
                        GameObject boat = Instantiate(smallBoat, new Vector3(LeftSpawn.transform.position.x, seaLevel + 0.1f, -0.3f), Quaternion.identity) as GameObject;
                        boat.name = smallBoat.name;
                        boat.transform.parent = this.transform;
                        boats.Add(boat);
                        boatsAfloat++;
                        boatToSpawn++;
                    }
                    else if (!rightSpawning)
                    {
                        GameObject boat = Instantiate(smallBoat, new Vector3(RightSpawn.transform.position.x, seaLevel + 0.1f, -0.3f), Quaternion.identity) as GameObject;
                        boat.name = smallBoat.name;
                        boat.transform.parent = this.transform;
                        boats.Add(boat);
                        boatsAfloat++;
                        boatToSpawn++;
                    }
                    break;
                case 'm':
                    if (!leftSpawning)
                    {
                        GameObject boat = Instantiate(mediumBoat, new Vector3(LeftSpawn.transform.position.x, seaLevel + 0.1f, -0.3f), Quaternion.identity) as GameObject;
                        boat.name = mediumBoat.name;
                        boat.transform.parent = this.transform;
                        boats.Add(boat);
                        boatsAfloat++;
                        boatToSpawn++;
                    }
                    else if (!rightSpawning)
                    {
                        GameObject boat = Instantiate(mediumBoat, new Vector3(RightSpawn.transform.position.x, seaLevel + 0.1f, -0.3f), Quaternion.identity) as GameObject;
                        boat.name = mediumBoat.name;
                        boat.transform.parent = this.transform;
                        boats.Add(boat);
                        boatsAfloat++;
                        boatToSpawn++;
                    }
                    break;
                case 'l':
                    if (!leftSpawning)
                    {
                        GameObject boat = Instantiate(largeBoat, new Vector3(LeftSpawn.transform.position.x, seaLevel + 0.1f, -0.3f), Quaternion.identity) as GameObject;
                        boat.name = largeBoat.name;
                        boat.transform.parent = this.transform;
                        boats.Add(boat);
                        boatsAfloat++;
                        boatToSpawn++;
                    }
                    else if (!rightSpawning)
                    {
                        GameObject boat = Instantiate(largeBoat, new Vector3(RightSpawn.transform.position.x, seaLevel + 0.1f, -0.3f), Quaternion.identity) as GameObject;
                        boat.name = largeBoat.name;
                        boat.transform.parent = this.transform;
                        boats.Add(boat);
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
            GameObject boat;
            switch (boatOrder[boatToSpawn])
            {
                case 's':
                    boat = Instantiate(smallBoat, new Vector3(-10 + (5 * i), seaLevel + 0.1f, -0.3f), Quaternion.identity) as GameObject;
                    boat.name = smallBoat.name;
                    boat.transform.parent = this.transform;
                    boats.Add(boat);
                    boatsAfloat++;
                    boatToSpawn++;
                    break;
                case 'm':
                    boat = Instantiate(mediumBoat, new Vector3(-10 + (5 * i), seaLevel + 0.1f, -0.3f), Quaternion.identity) as GameObject;
                    boat.name = mediumBoat.name;
                    boat.transform.parent = this.transform;
                    boats.Add(boat);
                    boatsAfloat++;
                    boatToSpawn++;
                    break;
                case 'l':
                    boat = Instantiate(largeBoat, new Vector3(-10 + (5 * i), seaLevel + 0.1f, -0.3f), Quaternion.identity) as GameObject;
                    boat.name = largeBoat.name;
                    boat.transform.parent = this.transform;
                    boats.Add(boat);
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

    public void Reset()
    {
        int size = boats.Count;
        for (int i = 0; i < boats.Count; i++)
        {
            if(boats[i] != null)
            {
                Destroy(boats[i]);
            }
        }
        boats.Clear();
        boatToSpawn = 0;
        boatsLeft = boatsInLevel;
        StarterShips();
    }

    public void DisableBoats(bool active)
    {
        for (int i = 0; i < boats.Count; i++)
        {
            if (boats[i] != null)
            {
                boats[i].gameObject.SetActive(active);
            }
        }
    }

    public void  NextLevel()
    {
        levelNumber = LevelManager.Instance.levelChosen;
        GetLevelData();
        boatsLeft = boatsInLevel;
        boatToSpawn = 0;
        Reset();
    }
}
