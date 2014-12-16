using UnityEngine;
using System.Collections;

public class BlockAccess : MonoBehaviour
{
    public GameObject image;
    GameObject background;
    public bool checkDomain = true;
    public bool fullURL = true;
    public string[] DomainList;
    public string message;
    private bool illegalCopy = true;

    public GUISkin Skin;

    private void Start()
    {
        print(Application.absoluteURL);
        if (Application.isWebPlayer && checkDomain)
        {
            int i = 0;
            for (i = 0; i < DomainList.Length; i++)
            {
                if (Application.absoluteURL == DomainList[i])
                {
                    illegalCopy = false;
                }
                else if (Application.absoluteURL.Contains(DomainList[i]) && !fullURL)
                {
                    illegalCopy = false;
                }
            }
        }
    }

    private void OnGUI()
    {
        GUI.skin = Skin;
        if (illegalCopy && background == null)
        {
            GUI.Label(new Rect(Screen.width * 0.1f, Screen.height - Screen.height * 0.2f, 2000, 200), message);
            background = (GameObject)Instantiate(image);
        }
        else
        {
            Application.LoadLevel("intro");
        }
    }
}
