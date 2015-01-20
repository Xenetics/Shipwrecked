using UnityEngine;
using System.Collections;

public class CloudButtonBehaviour : MonoBehaviour 
{
    [SerializeField]
    private GameObject highscoreButton;
    [SerializeField]
    private int speed;
    [SerializeField]
    private float cloudMaxHeight;
    [SerializeField]
    private float cloudMinHeight;
    private float cloudLeftBoundry = -12.0f;
    private float cloudRightBoundry = 12.0f;

	void Start () 
    {
	
	}
	
	void Update () 
    {
        highscoreButton.transform.Translate(Time.deltaTime * speed, 0, 0);
        if (highscoreButton.transform.position.x >= cloudRightBoundry)
        {
            float randomY = Random.Range(cloudMaxHeight, cloudMinHeight);
            Vector3 movePos = new Vector3(cloudLeftBoundry, randomY, -0.3f);
            highscoreButton.transform.position = movePos;
        }
	}
}
