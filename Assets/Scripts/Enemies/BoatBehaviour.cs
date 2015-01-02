using UnityEngine;
using System.Collections;

public class BoatBehaviour : MonoBehaviour 
{
    public GameObject entity;
    public bool dir { get; set; }
    public float surfaceLevel { get; set; }
    public bool bobDir { get; set; }
    public bool swayDir { get; set; }
    public float timeToEat { get; set; }

    void Awake()
    {
        entity = this.gameObject;
        dir = StartDir();
        bobDir = StartBool();
        swayDir = StartBool();
        surfaceLevel = entity.transform.position.y;
    }

	void Start () 
    {
	
	}
	
	void Update () 
    {
	
	}

    public void Avoiddance(Sensor left, Sensor right)
    {
        if (left.isColliding)
        {
            dir = true;
        }
        else if (right.isColliding)
        {
            dir = false;
        }
    }

    public void wander(float speed)
    {
        Vector3 entityPos = entity.transform.position;

        if(entityPos.x <= (-10f + entity.transform.localScale.x * 0.5f))
        {
            dir = true;
        }
        else if (entityPos.x >= (10f - entity.transform.localScale.x * 0.5f))
        {
            dir = false;
        }

        if(dir)
        {
            transform.Translate(Time.deltaTime * speed, 0, 0, null);
        }
        else
        {
            transform.Translate(-(Time.deltaTime * speed), 0, 0, null);
        }
    }

    public void Bobbing(float speed)
    {
        Vector3 entityPos = entity.transform.position;

        if (entityPos.y <= (surfaceLevel - entity.transform.localScale.y * 0.1f))
        {
            bobDir = true;
        }
        else if (entityPos.y >= (surfaceLevel + entity.transform.localScale.y * 0.025f))
        {
            bobDir = false;
        }

        if(bobDir)
        {
            transform.Translate(0, Time.deltaTime * speed, 0, null);
        }
        else
        {
            transform.Translate(0, -(Time.deltaTime * speed), 0, null);
        }
    }

    public void Sway(float speed, float angle)
    {
        Transform entityRot = entity.transform;

        if (entityRot.eulerAngles.z <= 360 - angle && entityRot.eulerAngles.z > 150)
        {
            swayDir = true;
        }
        else if (entityRot.eulerAngles.z >= angle && entityRot.eulerAngles.z < 150)
        {
            swayDir = false;
        }

        if (swayDir)
        {
            entityRot.transform.Rotate(0, 0, Time.deltaTime * speed);
        }
        else
        {
            entityRot.transform.Rotate(0, 0, -(Time.deltaTime * speed));
        }

        entity.transform.rotation = entityRot.rotation;
    }

    public void Sink(float speed, float swaySpeed)
    {
        Transform temp = entity.transform;

        if (temp.position.y > surfaceLevel - 8)
        {
            transform.Translate(0, -(Time.deltaTime * speed), 0, null);

            if (swayDir)
            {
                temp.transform.Rotate(0, 0, Time.deltaTime * swaySpeed);
            }
            else
            {
                temp.transform.Rotate(0, 0, -(Time.deltaTime * swaySpeed));
            }
        }
    }

    private bool StartDir()
    {
        if(entity.transform.position.x > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool StartBool()
    {
        int rando = Random.Range(1, 10);

        if(rando > 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
