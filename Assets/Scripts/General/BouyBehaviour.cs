using UnityEngine;
using System.Collections;

public class BouyBehaviour : BoatBehaviour 
{
    [SerializeField]
    private float bobSpeed = 0.1f;
    [SerializeField]
    private float swaySpeed = 5f;
    [SerializeField]
    private float maxSwayAngle = 5f;

    private bool clicked = false;

    void Start()
    {

    }

    void Update()
    {
        if (!clicked)
        {
            Sway(swaySpeed, maxSwayAngle);
            Bobbing(bobSpeed);
        }
    }

    void LateUpdate()
    {

    }
}
