using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour 
{
    public bool left;

	void Start () 
    {
	
	}

	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(left)
        {
            BoatManager.Instance.leftSpawning = true;
        }
        else
        {
            BoatManager.Instance.rightSpawning = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (left)
        {
            BoatManager.Instance.leftSpawning = false;
        }
        else
        {
            BoatManager.Instance.rightSpawning = false;
        }
    }
}
