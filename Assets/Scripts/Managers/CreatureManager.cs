using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatureManager : MonoBehaviour
{
    private static CreatureManager instance = null;
    public static CreatureManager Instance { get { return instance; } }

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
        levelNumber = LevelManager.Instance.levelChosen;
        GetLevelData();
    }

    public int creaturesInLevel { get; set; } // amount of creatures to spawn
    private char[] creatureOrder; // char tags for creatures in what order
    private int creatureToSpawn = 0; // which creature to spawn

    // prephabs 
    [SerializeField]
    private GameObject smallCreature;
    [SerializeField]
    private GameObject mediumCreature;
    [SerializeField]
    private GameObject largeCreature;

    private float creatureMaxHeight = 6.0f;
    private float creatureMinHeight = 2.0f;
    private float creatureLeftBound = -10.0f;
    private float creatureRightBound = 10.0f;

    private int levelNumber; // what level we are on

    public int creaturesAlive { get; set; } // creatures alive currently

    private List<GameObject> creatures;

    void Start()
    {
        creatures = new List<GameObject>();
        SpawnCreatures();
    }

    void Update()
    {

    }

    private void SpawnCreatures() 
    {
        creaturesAlive = creaturesInLevel;
        for (int i = 0; i < creaturesInLevel; i++)
        {
            GameObject newCreature;
            float randomX = Random.RandomRange(creatureLeftBound, creatureRightBound);
            float randomY = Random.RandomRange(creatureMinHeight, creatureMaxHeight);
            switch (creatureOrder[creatureToSpawn])
            {
                case's':
                    newCreature = Instantiate(smallCreature, new Vector3(randomX, randomY, -0.3f), Quaternion.identity) as GameObject;
                    newCreature.name = smallCreature.name;
                    newCreature.transform.parent = this.transform;
                    creatures.Add(newCreature);
                    creatureToSpawn++;
                    break;
                case'm':
                    newCreature = Instantiate(mediumCreature, new Vector3(randomX, randomY, -0.3f), Quaternion.identity) as GameObject;
                    newCreature.name = mediumCreature.name;
                    newCreature.transform.parent = this.transform;
                    creatures.Add(newCreature);
                    creatureToSpawn++;
                    break;
                case'l':
                    newCreature = Instantiate(largeCreature, new Vector3(randomX, randomY, -0.3f), Quaternion.identity) as GameObject;
                    newCreature.name = largeCreature.name;
                    newCreature.transform.parent = this.transform;
                    creatures.Add(newCreature);
                    creatureToSpawn++;
                    break;
            }
        }
    }

    public void Reset()
    {
        int size = creatures.Count;
        for (int i = 0; i < creatures.Count; i++)
        {
            if (creatures[i] != null)
            {
                Destroy(creatures[i]);
            }
        }
        creatures.Clear();
        creatureToSpawn = 0;
        SpawnCreatures();
    }

    public void DisableCreatures(bool active)
    {
        for (int i = 0; i < creatures.Count; i++)
        {
            if (creatures[i] != null)
            {
                creatures[i].gameObject.SetActive(active);
            }
        }
    }

    public void KillCreature(GameObject target)
    {
        for(int i = 0; i < creatures.Count; i++)
        {
            if (target == creatures[i])
            {
                creatures[i].gameObject.SetActive(false);
                creaturesAlive--;
            }
        }
    }

    public void NextLevel()
    {
        levelNumber = LevelManager.Instance.levelChosen;
        GetLevelData();
        creatureToSpawn = 0;
        Reset();
    }

    private void GetLevelData() // get how many ships, and what types of ships, and in what order for this level
    {
        LevelData temp = LevelManager.Instance.GetLevel();
        creaturesInLevel = temp.totalCreatures;
        creatureOrder = temp.creatureOrder;
    }
}

