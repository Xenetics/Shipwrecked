using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    public GameObject entity;
    private bool isAlive = true;
    private bool grounded;
    private bool attacking = false;
    private bool eating = false;
    private float eatTime;
    public float speed = 150.0F;
    public float attackSpeed = 50.0F;
    public float gravity = 10.0f;
    private float hSpeed;
    private float groundLevel;
    private float seaLevel = 7f;

    void Awake()
    {
        groundLevel = entity.transform.position.y;
    }

	void Start () 
    {
	
	}
	
	void Update () 
    {
        if (isAlive)
        {
            if (grounded)
            {
                hSpeed = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
                hSpeed *= Time.deltaTime;
                entity.transform.Translate(hSpeed, 0, 0, null);

                if (Input.GetButtonDown("Jump"))
                {
                    attacking = true;
                }
            }
            else
            {
                if (!eating)
                {
                    entity.transform.Translate(0, -(Time.deltaTime * gravity), 0, null);
                }
                else
                {
                    eatTime -= Time.deltaTime;
                    if(eatTime <= 0.0f)
                    {
                        eating = false;
                    }
                }
            }
            
            if(attacking)
            {
                entity.transform.Translate(0, Time.deltaTime * attackSpeed, 0, null);
            }

            if (entity.transform.position.y >= seaLevel)
            {
                attacking = false;
            }

            if (entity.transform.position.y <= groundLevel)
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }
        }

        // keep player on screen
        if(entity.transform.position.x > 9.5f) // right border
        {
            Vector3 temp = new Vector3(9.5f, entity.transform.position.y, entity.transform.position.z);
            entity.transform.position = temp;
        }
        else if(entity.transform.position.x < -9.5) // left border
        {
            Vector3 temp = new Vector3(-9.5f, entity.transform.position.y, entity.transform.position.z);
            entity.transform.position = temp;
        }

        if (entity.transform.position.y < -1f) // bottom border
        {
            Vector3 temp = new Vector3(entity.transform.position.x, -1f, entity.transform.position.z);
            entity.transform.position = temp;
        }
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "enemy" && !eating)
        {
            if (other.gameObject.transform.position.x > -10 && other.gameObject.transform.position.x < 10)
            {
                EnemyStats target = other.gameObject.GetComponent<EnemyStats>();
                eating = true;
                eatTime = target.timeToEat;
                target.isAlive = false;
                other.gameObject.collider.enabled = false;
                BoatManager.Instance.boatsAfloat--;
            }
        }
    }
}
