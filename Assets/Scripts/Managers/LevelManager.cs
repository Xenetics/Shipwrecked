using UnityEngine;
using System.Collections;
using System.Xml;
using System.Net;
using System.IO;

public struct LevelData
{
    public int level;
    public int totalShips;
    public char[] shipOrder;
    public int totalCreatures;
    public char[] creatureOrder;
}

public class LevelManager : MonoBehaviour 
{
    private static LevelManager instance = null;
    public static LevelManager Instance { get { return instance; } }

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

        string textData = levelXML.text;
        ParseXML(textData);
        levelsAvailable = PlayerPrefs.GetInt("level");
    }

    [SerializeField]
    private TextAsset levelXML;

    public LevelData[] levels;

    public int levelChosen { get; set; }
    public int totalLevels { get; set; }
    public int levelsAvailable { get; set; }

	void Start () 
    {
        
	}

    private void ParseXML(string xml) // parses the data out of the Xml
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(new StringReader(xml));
        string xmlPathPattern = "//levels/level";
        XmlNodeList myNodeList = doc.SelectNodes(xmlPathPattern);

        levels = new LevelData[myNodeList.Count];
        totalLevels = myNodeList.Count;
        int count = 0;
        foreach (XmlNode node in myNodeList)
        {
            levels[count] = ParseNode(node);
            count++;
        }
    }
	
    private LevelData ParseNode(XmlNode node) // creates a level out of a node
    {
        XmlNode levelNode = node.FirstChild;
        XmlNode shipsNode = levelNode.NextSibling;
        XmlNode creaturesNode = shipsNode.NextSibling;

        LevelData temp = new LevelData();

        temp.level = int.Parse(levelNode.InnerXml);
        temp.totalShips = shipsNode.InnerXml.Length;
        temp.shipOrder = new char[temp.totalShips];
        int count = 0;
        foreach(char ship in shipsNode.InnerXml)
        {
            temp.shipOrder[count] = shipsNode.InnerXml[count];
            count++;
        }

        temp.totalCreatures = creaturesNode.InnerXml.Length;
        temp.creatureOrder = new char[temp.totalCreatures];
        count = 0;
        foreach (char creature in creaturesNode.InnerXml)
        {
            temp.creatureOrder[count] = creaturesNode.InnerXml[count];
            count++;
        }

        return temp;
    }


    public LevelData GetLevel() // returns chosen level data
    {
        return levels[levelChosen - 1];
    }

    public LevelData GetLevel(int level) // returns a specific level // not sure if needed yet
    {
        int count = 0;
        foreach (LevelData lvl in levels)
        {
            if (lvl.level == level - 1)
            {
                return levels[count];
            }
            count++;
        }
        return new LevelData();
    }
    
    public void SaveLevelProgress(int level)
    {
        PlayerPrefs.SetInt("level", level);
    }
}
