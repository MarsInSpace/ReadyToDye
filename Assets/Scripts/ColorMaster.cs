using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMaster : MonoBehaviour
{

    public static ColorMaster Instance;

    private void Awake()
    {
        if (ColorMaster.Instance == null)
            ColorMaster.Instance = this;
        else
            Destroy(this.gameObject);
    }

    public Color[] ColorVector;



    public GameColorTypes GetRespectiveColor(GameColorTypes colorType)
    {
        switch (colorType)
        {
            case GameColorTypes.DarkBlue:
                return GameColorTypes.Orange;
            case GameColorTypes.Orange:
                return GameColorTypes.DarkBlue;
            case GameColorTypes.LightBlue:
                return GameColorTypes.Beige;
            case GameColorTypes.Beige:
                return GameColorTypes.LightBlue;
            default:
                return colorType;
        }
    }
}

public enum GameColorTypes
{
    DarkBlue,
    Beige,
    Orange,
    LightBlue
}

public class GameColor
{
    

    private string name; // field
    public string Name   // property
    {
        get { return name; }   // get method
    }


    private GameColorTypes type;

    public GameColorTypes Type
    {
        get { return type; }  
    }


    private Color color; 

    public Color Color
    {
        get { return color; }
    }


    public GameColor(GameColorTypes setType)
    {
        this.type = setType;

        switch (this.type)
        {
            case GameColorTypes.DarkBlue:
                color = ColorMaster.Instance.ColorVector[0];
                name = "DarkBlue";
                break;
            case GameColorTypes.Beige:
                color = ColorMaster.Instance.ColorVector[1];
                name = "Beige";
                break;
            case GameColorTypes.Orange:
                color = ColorMaster.Instance.ColorVector[2];
                name = "Orange";
                break;
            case GameColorTypes.LightBlue:
                color = ColorMaster.Instance.ColorVector[3];
                name = "LightBlue";
                break;
            default:
                return;

        }
    } 
}

public static class Extension
{
    public static bool IsEqualTo(this GameColor one, GameColor two)
    {
        Debug.Log("Checking colors: " + one + ", " + two);

        return (one.Color.r == two.Color.r && one.Color.g == two.Color.g && one.Color.b == two.Color.b && one.Color.a == two.Color.a);
    }
}
