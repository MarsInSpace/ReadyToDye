using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMaster : MonoBehaviour
{
    //Master to manage/provide any information related to color system
    public static ColorMaster Instance;

    private void Awake()
    {
        if (ColorMaster.Instance == null)
            ColorMaster.Instance = this;
        else
            Destroy(this.gameObject);
    }


    //the colors used within the Level
    public Color[] ColorVector;


    /// <summary>
    /// returns the associated color of the color pair with the given color
    /// </summary>
    /// <param name="colorType">color to get the associated color for</param>
    /// <returns></returns>
    public GameColorTypes GetRespectiveColor(GameColorTypes colorType)
    {
        switch (colorType)
        {
            case GameColorTypes.ColorA:
                return GameColorTypes.ColorC;
            case GameColorTypes.ColorC:
                return GameColorTypes.ColorA;
            case GameColorTypes.ColorD:
                return GameColorTypes.ColorB;
            case GameColorTypes.ColorB:
                return GameColorTypes.ColorD;
            default:
                return colorType;
        }
    }
}

public enum GameColorTypes
{
    ColorA,
    ColorB,
    ColorC,
    ColorD
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

    //Constructor for GameColors
    public GameColor(GameColorTypes setType)
    {
        this.type = setType;

        switch (this.type)
        {
            case GameColorTypes.ColorA:
                color = ColorMaster.Instance.ColorVector[0];
                name = "ColorA";
                break;
            case GameColorTypes.ColorB:
                color = ColorMaster.Instance.ColorVector[1];
                name = "ColorB";
                break;
            case GameColorTypes.ColorC:
                color = ColorMaster.Instance.ColorVector[2];
                name = "ColorC";
                break;
            case GameColorTypes.ColorD:
                color = ColorMaster.Instance.ColorVector[3];
                name = "ColorD";
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
