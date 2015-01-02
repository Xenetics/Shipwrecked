using UnityEngine;
using System.Collections;

public class WaterSway : MonoBehaviour 
{
    public GameObject water;
    public float swaySpeed = 1;
    public float slowRate = 0.1f;
    private float speed;
    private bool dir = false; // false is left
    private bool accelerate = true;

	void Start () 
    {
        speed = swaySpeed;
	}
	
	void Update () 
    {
        Vector3 waterPos = water.transform.position;

        // water pos adjustment
        if (!dir)
        {
            waterPos.x -= Time.deltaTime * speed;
        }
        else
        {
            waterPos.x += Time.deltaTime * speed;
        }

        // keep water in bounds
        if(water.transform.position.x >= 9)
        {
            dir = false;
        }
        else if (water.transform.position.x <= -9)
        {
            dir = true;
        }

        // accellerate and decellerate water
        if (!accelerate)
        {
            speed -= Time.deltaTime * slowRate;
        }
        else
        {
            speed += Time.deltaTime * slowRate;
        }

        // swap direction of sway based on speed
        if(speed <= 0.15f)
        {
            accelerate = true;
            dir = !dir;
        }
        else if(speed >= swaySpeed * 0.95f)
        {
            accelerate = false;
        }

        water.transform.position = waterPos;
	}
}
