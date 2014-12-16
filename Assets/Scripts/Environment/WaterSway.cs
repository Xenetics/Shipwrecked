using UnityEngine;
using System.Collections;

public class WaterSway : MonoBehaviour 
{
    public GameObject water;
    public float swaySpeed = 5;
    private bool dir = false; // false is left

	void Start () 
    {
	
	}
	
	void Update () 
    {
        Vector3 waterPos = water.transform.position;

        if (!dir)
        {
            waterPos.x -= Time.deltaTime * swaySpeed;
        }
        else
        {
            waterPos.x += Time.deltaTime * swaySpeed;
        }

        if(water.transform.position.x >= 9)
        {
            dir = false;
        }
        else if (water.transform.position.x <= -9)
        {
            dir = true;
        }


        water.transform.position = waterPos;
	}
}
