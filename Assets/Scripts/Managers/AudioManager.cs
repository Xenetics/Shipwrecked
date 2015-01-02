using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour 
{
	private static AudioManager instance = null;
	public static AudioManager Instance { get { return instance; } }
	
	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		if(instance != null && instance != this)
		{
			Destroy (this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}

        musicOn = true;
        leaveState = false;
	}

    private int musicAmount = 1;
    public GameObject[] MusicPlayer; // maked this an array to use multiple musics so its a more general sound manager.
    private GameObject[] Musics;
    public AudioClip[] Sounds;
    public bool musicOn { get; set; }
    public bool leaveState { get; set; }
     
	void Start () 
	{
        Musics = new GameObject[musicAmount];
        //FillMusics();
	}
	
	void Update () 
	{
        //ApplyMute(); used for multi scene
	}

    void LateUpdate()
    {

    }

	public void PlaySound(string toPlay)
	{
        for(int i = 0; i < Sounds.Length; ++i)
        {
            if(toPlay == Sounds[i].name)
            {
                AudioSource.PlayClipAtPoint(Sounds[i], Camera.main.transform.position);
            }
        }
	}

    private void ApplyMute()
    {
        for(int i = 0; i < Musics.Length; ++i)
        {
            string tempName = Application.loadedLevelName + "(Clone)";
            if (musicOn && tempName == Musics[i].gameObject.name)
            {
                Musics[i].audio.mute = false;
                if (!Musics[i].audio.isPlaying)
                {
                    Musics[i].audio.Play();
                }
            }
            else
            {
                Musics[i].transform.parent = gameObject.transform;
                Musics[i].audio.mute = true;
                if (Musics[i].audio.isPlaying)
                {
                    Musics[i].audio.Stop();
                }
            }
        }
    }
    /*
    private void FillMusics()
    {
        GameObject add0 = (GameObject)Instantiate(MusicPlayer, Camera.main.transform.position, Quaternion.identity);
        Musics[0] = add0;
        Musics[0].transform.parent = gameObject.transform;
        //Musics[0].audio.mute = true;
        Musics[0].audio.mute = false;
    }
    */
    public void ToggleMusic(bool toggle)
    {
        musicOn = toggle;
    }

    public bool IsMusicOn()
    {
        return musicOn;
    }
}
