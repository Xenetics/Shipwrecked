using UnityEngine;
using System.Collections;

public class ButtonBehaviour : BoatBehaviour 
{
    private bool clicked = false;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float bobSpeed = 1f;
    [SerializeField]
    private float swaySpeed = 1f;
    [SerializeField]
    private float maxSwayAngle = 3f;
    [SerializeField]
    private float sinkSpeed = 1f;
    [SerializeField]
    private Sensor leftSensor;
    [SerializeField]
    private Sensor rightSensor;
    private float deathTimer = 0;

    void Start()
    {

    }

    void Update()
    {
        if (!clicked)
        {
            wander(speed);
            Sway(swaySpeed, maxSwayAngle);
            Bobbing(bobSpeed);
        }
        else
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer <= 0.0f)
            {
                Sink(sinkSpeed, swaySpeed);
            }
        }
    }

    void LateUpdate()
    {
        if (!clicked)
        {
            Avoiddance(leftSensor, rightSensor);
        }
    }
}
