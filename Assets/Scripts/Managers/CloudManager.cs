using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudManager : MonoBehaviour 
{
    [SerializeField]
    private GameObject cloud;
    [SerializeField]
    private int cloudDensity;
    [SerializeField]
    private int cloudMaxSpeed;
    [SerializeField]
    private float cloudMaxHeight;
    [SerializeField]
    private float cloudMinHeight;
    private float cloudLeftBoundry = -15.0f;
    private float cloudRightBoundry = 15.0f;

    private List<GameObject> Clouds;
    private float[] cloudSpeed;

    private static CloudManager instance = null;
    public static CloudManager Instance { get { return instance; } }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        Clouds = new List<GameObject>();
        cloudSpeed = new float[cloudDensity];
        InitClouds();
	}
	
	void Update () 
    {
        int count = 0;
	    foreach(GameObject cloud in Clouds)
        {
            cloud.transform.Translate(Time.deltaTime * cloudSpeed[count], 0, 0);
            if(cloud.transform.position.x >= cloudRightBoundry)
            {
                float randomY = Random.Range(cloudMaxHeight, cloudMinHeight);
                Vector3 movePos = new Vector3(cloudLeftBoundry, randomY, -0.3f);
                cloud.transform.position = movePos;
                cloudSpeed[count] = Random.Range(cloudMaxSpeed * 0.1f, cloudMaxSpeed);
            }
            count++;
        }
	}

    private void InitClouds()
    {
        for(int i = 0; i < cloudDensity; ++i)
        {
            float randomX = Random.Range(cloudLeftBoundry, cloudRightBoundry);
            float randomY = Random.Range(cloudMaxHeight, cloudMinHeight);
            GameObject newCloud = Instantiate(cloud, new Vector3(randomX, randomY, -0.3f), Quaternion.identity) as GameObject;
            newCloud.name = cloud.name;
            newCloud.transform.parent = this.transform;
            Clouds.Add(newCloud);
            cloudSpeed[i] = Random.Range(cloudMaxSpeed * 0.1f, cloudMaxSpeed);
        }
    }
}
