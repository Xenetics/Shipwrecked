using UnityEngine;
using System.Collections;

public class WhaleMovement : CreatureBehaviour 
{
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private EnemyStats stats;

    private bool chooseDest;
    private float destTimer = 0.5f;

	void Start () 
    {
	
	}

	void Update () 
    {
        if (GameManager.WhatState() == "playing" && InGameUIManager.Instance.paused == false)
        {
            if (!InBounds() && !chooseDest)
            {
                dest = NewDest();
                chooseDest = true;
            }
            else if (Vector3.Distance(entity.transform.position, dest) < 0.1f && !chooseDest)
            {
                dest = NewDest();
                chooseDest = true;
            }

            destTimer -= Time.deltaTime;
            if(destTimer < 0)
            {
                destTimer = 0.5f;
                chooseDest = false;
            }
            
            wander(speed);
        }
	}
}
