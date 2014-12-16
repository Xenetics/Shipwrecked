using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour 
{
    public GUISkin guiSkin;
    private static GuiManager instance = null;
    public static GuiManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }
    
    public static GUISkin GetSkin()
    {
        return instance.guiSkin;
    }
}
