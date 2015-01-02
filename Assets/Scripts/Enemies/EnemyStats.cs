using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour 
{
    public float timeToEat = 1.0f;
    public int pointWorth = 100;
    public bool isAlive { get; set; }

    void Awake()
    {
        isAlive = true;
    }
	
	void Start () 
    {
	
	}
	
	void Update () 
    {
	
	}
}
