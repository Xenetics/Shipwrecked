using UnityEngine;
using System.Collections;

public class RiseingHorns : MonoBehaviour 
{
    public float speed = 10.0f;
	void Start () 
    {
	
	}

	void Update () 
    {
        Vector3 temp = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if(temp.y < 0.5f)
        {
            temp.y += Time.deltaTime * speed;
            transform.position = temp;
        }
	}
}
