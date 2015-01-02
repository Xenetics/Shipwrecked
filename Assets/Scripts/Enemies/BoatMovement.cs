using UnityEngine;
using System.Collections;

public class BoatMovement : BoatBehaviour 
{
    public float speed = 1f;
    public float bobSpeed = 1f;
    public float swaySpeed = 1f;
    public float maxSwayAngle = 3f;
    public float sinkSpeed = 1f;
    public Sensor leftSensor;
    public Sensor rightSensor;
    private EnemyStats stats;
    private float deathTimer;

	void Start () 
    {
        stats = gameObject.GetComponentInChildren<EnemyStats>();
        deathTimer = stats.timeToEat;
	}
	
	void Update () 
    {
        if (stats.isAlive)
        {
            wander(speed);
            Sway(swaySpeed, maxSwayAngle);
            Bobbing(bobSpeed);
        }
        else
        {
            deathTimer -= Time.deltaTime;
            if(deathTimer <= 0.0f)
            {
                Sink(sinkSpeed, swaySpeed);
            }
        }
	}

    void LateUpdate()
    {
        if (stats.isAlive)
        {
            Avoiddance(leftSensor, rightSensor);
        }
    }
}
