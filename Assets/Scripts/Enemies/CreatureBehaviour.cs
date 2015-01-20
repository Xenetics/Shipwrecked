using UnityEngine;
using System.Collections;

public class CreatureBehaviour : MonoBehaviour 
{
    public GameObject entity;
    public float surfaceLevel { get; set; }
    public bool swayDir { get; set; }
    private float topBound = 6.0f;
    private float bottomBound = 2.0f;
    private float leftBound = -10.0f;
    private float rightBound = 10.0f;

    public Vector3 dest { get; set; }

    void Awake()
    {
        entity = this.gameObject;
        swayDir = StartBool();
        surfaceLevel = entity.transform.position.y;
        dest = NewDest();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void wander(float speed)
    {
        Quaternion tempRot;
        if (entity.transform.position.x >= dest.x)
        {
            tempRot = Quaternion.identity;
            entity.transform.rotation = tempRot;
        }
        else
        {
            tempRot = Quaternion.Euler(0, 180, 0);
            entity.transform.rotation = tempRot;
        }

        entity.transform.position = Vector3.MoveTowards(entity.transform.position, dest, Time.deltaTime * speed);
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

    private bool StartBool()
    {
        int rando = Random.Range(1, 10);

        if (rando > 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool InBounds()
    {
        if (entity.transform.position.x < rightBound && entity.transform.position.x > leftBound && entity.transform.position.y < topBound && entity.transform.position.y > bottomBound)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector3 NewDest()
    {
        float randomX = Random.RandomRange(leftBound, rightBound);
        float randomY = Random.RandomRange(bottomBound, topBound);

        return new Vector3(randomX, randomY, 0);
    }
}
