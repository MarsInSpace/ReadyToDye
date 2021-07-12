using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FontColorMaster : MonoBehaviour
{
    public static FontColorMaster Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
            Destroy(this.gameObject);
    }

    public Color LightBlue = new Color(167, 191, 227, 255);
    public Color DarkBlue = new Color(65, 60, 119, 255);
    public Color Beige = new Color(235, 207, 146, 255);
    public Color Orange = new Color(226, 166, 96, 255);
    public Color Black = new Color(35, 32, 53, 255);
    public Color White = new Color(248, 239, 219, 255);
}
